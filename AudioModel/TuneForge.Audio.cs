// Import necessary namespaces
using NAudio.Wave;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        // Audio output device and file reader
        private WaveOutEvent outputDevice;
        private AudioFileReader _audioFile;

        // Timer for updating playback time
        private readonly DispatcherTimer _timer = new();

        // Current and new music file paths
        private string _currentMusicPath;
        private string _newMusicPath;

        // Playback state flags
        private bool _isMusicPlaying;
        private bool _isSoundOn;
        private bool _userIsDragging;
        private bool _IsSelectedSongFavorite;
        private bool _isSliderEnabled = false;

        // StartPage instance
        private StartPage _startPage = new();

        // Music path property (safe with null fallback)
        public string CurrentMusicPath
        {
            get => _currentMusicPath ?? string.Empty;
            set => _currentMusicPath = value;
        }

        // Public access to slider enabled state
        public bool isSliderEnabled { get => _isSliderEnabled; set => _isSliderEnabled = value; }

        // Initialize timer to tick every 500ms
        private void InitTimerMusic()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += TimerTime_Tick;
        }

        // Extract album art from music file using TagLib
        private BitmapImage GetAlbumArt(string path)
        {
            if (!File.Exists(path)) return null;

            using var tagFile = TagLib.File.Create(path);
            if (tagFile.Tag.Pictures.Length == 0) return null;

            var bin = tagFile.Tag.Pictures[0].Data.Data;
            if (bin.Length == 0) return null;

            using var ms = new MemoryStream(bin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        // Update album art image in ViewModel
        public void UpdateAlbumArt(string path)
        {
            var albumImage = GetAlbumArt(path);
            if (albumImage == null) return;

            _viewModel.AlbumArt = albumImage;
        }

        // Extract artist and song title using TagLib (fallback to filename parsing if tags are empty)
        public void TakeArtistSongName(string path)
        {
            try
            {
                var file = TagLib.File.Create(path);
                string artist = file.Tag.FirstPerformer ?? string.Empty;
                string title = file.Tag.Title ?? string.Empty;

                if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(title))
                {
                    var fileName = Path.GetFileNameWithoutExtension(path);
                    var parts = fileName.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        artist = parts[0].Trim();
                        title = parts[1].Trim();
                    }
                    else
                    {
                        title = fileName;
                    }
                }

                _viewModel.Artist = artist;
                _viewModel.Title = title;
            }
            catch
            {
                _viewModel.Artist = "";
                _viewModel.Title = "";
            }
        }

        // Initialize audio player with selected file
        private void InitMusic(string path)
        {
            _audioFile = new AudioFileReader(CurrentMusicPath);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(_audioFile);
            outputDevice.PlaybackStopped += OnPlaybackStopped;
            _isSoundOn = true;
        }

        // Play the music
        private void PlayMusic()
        {
            if (_audioFile == null || outputDevice == null)
            {
                MessageBox.Show("Please, select a song.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            outputDevice.Volume = 1f;
            outputDevice.Play();
            _isMusicPlaying = true;
        }

        // Called when the slider value is changed manually by the user
        public void SliderChanged()
        {
            if (_audioFile == null || outputDevice == null) return;

            _isSliderEnabled = true; // allow slider interaction
            double frac = _startPage.MusicTrackBar.Value / _startPage.MusicTrackBar.Maximum;
            _audioFile.CurrentTime = TimeSpan.FromSeconds(frac * _audioFile.TotalTime.TotalSeconds);
            _viewModel.CurrentTime = _audioFile.CurrentTime.ToString(@"mm\:ss");
            _userIsDragging = false;
        }

        // Timer tick updates slider position and current time
        private void TimerTime_Tick(object sender, EventArgs e)
        {
            if (_audioFile == null || !_isMusicPlaying || _userIsDragging)
                return;

            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                double progress = _audioFile.CurrentTime.TotalSeconds / _audioFile.TotalTime.TotalSeconds;
                _viewModel.TrackPosition = progress * _startPage.MusicTrackBar.Maximum;

                _viewModel.CurrentTime = _audioFile.CurrentTime.ToString(@"mm\:ss");
                _viewModel.EndTime = _audioFile.TotalTime.ToString(@"mm\:ss");
            }
        }

        // Toggle mute/unmute
        public void ToggleSound(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null) return;

            _isSoundOn = !_isSoundOn;
            if (_isSoundOn)
            {
                outputDevice.Volume = 1f;
                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_new.png");
                _viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            else
            {
                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_c.png");
                _viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                outputDevice.Volume = 0f;
            }
        }

        // Called when the play button is clicked
        public void OnClickMusic(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentMusicPath))
            {
                MessageBox.Show("No music selected", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // If a new song was selected, stop and clean up the old one
            if (_newMusicPath != CurrentMusicPath)
                StopAndDisposeCurrentMusic();

            if (outputDevice == null || _audioFile == null)
            {
                try
                {
                    TakeArtistSongName(CurrentMusicPath);
                    InitMusic(_currentMusicPath);
                    PlayMusic();
                    UpdateAlbumArt(CurrentMusicPath);
                    _timer.Start();
                    _newMusicPath = CurrentMusicPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error playing audio: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Toggle pause/play
                if (_isMusicPlaying)
                {
                    _timer.Stop();
                    outputDevice.Pause();
                }
                else
                {
                    _timer.Start();
                    outputDevice.Play();
                }
                _isMusicPlaying = !_isMusicPlaying;
            }
        }

        // Stop and release resources for current audio
        private void StopAndDisposeCurrentMusic()
        {
            _timer.Stop();

            if (outputDevice != null)
            {
                outputDevice.PlaybackStopped -= OnPlaybackStopped;
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            _audioFile?.Dispose();
            _audioFile = null;

            _isMusicPlaying = false;
            _viewModel.TrackPosition = 0;
            _viewModel.CurrentTime = "00:00";
            _viewModel.EndTime = "00:00";

            // Set default album image if not available
            var defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/menu/musicLogo.jpg");
            if (File.Exists(defaultImagePath))
            {
                var image = new BitmapImage(new Uri(defaultImagePath, UriKind.Absolute));
                _viewModel.AlbumArt = image;
            }
            else
            {
                _viewModel.AlbumArt = null;
            }
        }

        // Restart song if playback was stopped unexpectedly
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _timer.Stop();

            if (_isMusicPlaying && _audioFile != null && outputDevice != null)
            {
                _timer.Start();
                outputDevice.Play();
                _isMusicPlaying = true;
            }
            else
            {
                _isMusicPlaying = false;
            }
        }

        // Set playback to start (0 sec)
        public void StartMusic(object sender, RoutedEventArgs e)
        {
            if (_audioFile != null)
            {
                _audioFile.CurrentTime = TimeSpan.Zero;
                outputDevice?.Pause();
            }
        }

        // Set playback to end (almost finish)
        public void EndMusic(object sender, RoutedEventArgs e)
        {
            if (_audioFile != null)
                _audioFile.CurrentTime = _audioFile.TotalTime - TimeSpan.FromMilliseconds(500);
        }

        // Toggle "favorite" status for current song
        public void SelectFavoriteSongToPlayList(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null || _audioFile == null) return;

            _IsSelectedSongFavorite = !_IsSelectedSongFavorite;
            string path = _IsSelectedSongFavorite
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\favorite_b.png")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\sidebar\\favorite_a.png");

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            _viewModel.FavoriteSong = bitmap;
        }

        // Restart song from beginning
        public void RepeatSong(object sender, RoutedEventArgs e)
        {
            if (_audioFile == null || outputDevice == null)
                return;

            _audioFile.Position = 0;
            _timer.Start();
            outputDevice.Play();
            _isMusicPlaying = true;
        }
    }
}

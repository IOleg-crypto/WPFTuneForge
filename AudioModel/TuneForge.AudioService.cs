using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;
using WinForm = System.Windows.Forms;

namespace WpfTuneForgePlayer.AudioModel
{
    public class AudioService
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader _audioFile;
        private TimerHelper _timer;
        private MusicViewModel _viewModel;
        private AudioMetaService _audioMetaService;
        private StartPage _startPage = new();
        private VolumeService volumeService;
        private MusicNavigationService musicNavigationService;
        private bool _isSoundOn;
        private bool _IsSelectedSongFavorite;
        private bool _isSliderEnabled = false;

        public bool isSliderEnabled { get => _isSliderEnabled; set => _isSliderEnabled = value; }
        public bool _isMusicPlaying;

        private string _currentMusicPath;
        private string _newMusicPath;

        private string FileName = "FavoriteSong.bin";

        public WaveOutEvent _outputDevice { get => outputDevice; set => outputDevice = value; }
        public AudioFileReader audioFile { get => _audioFile; set => _audioFile = value; }
        public TimerHelper _timerHelper { get => _timer; set => _timer = value; }
        public StartPage startPage { get => _startPage; set => _startPage = value; }

        public VolumeService _volumeService { get => volumeService; set => volumeService = value; }
        public MusicNavigationService _musicNavigationService { get => musicNavigationService; set => musicNavigationService = value; }

        public bool isSound { get => _isSoundOn; set => _isSoundOn = value; }

        public bool IsSelectedSongFavorite { get => _IsSelectedSongFavorite; set => _IsSelectedSongFavorite = value; }

        public AudioService(MusicViewModel viewModel)
        {
            _viewModel = viewModel;
            _audioMetaService = new AudioMetaService(_viewModel);
            volumeService = new VolumeService(this , _viewModel);
            musicNavigationService = new MusicNavigationService(_viewModel, this, _audioMetaService);
            _timer = new TimerHelper(TimeSpan.FromMilliseconds(400), this, _viewModel);
            
        }

        public string CurrentMusicPath
        {
            get => _currentMusicPath ?? string.Empty;
            set => _currentMusicPath = value;
        }

        public string NewMusicPath
        {
            get => _newMusicPath ?? string.Empty;
            set => _newMusicPath = value;
        }

        private void InitMusic(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                return;
            };
            SimpleLogger.Log("Init music - AudioFileReader and WaveOutEvent");
            _audioFile = new AudioFileReader(CurrentMusicPath);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(_audioFile);
            outputDevice.PlaybackStopped += OnPlaybackStopped;
            _isSoundOn = true;
        }
        private void PlayMusic()
        {
            SimpleLogger.Log("Play music");
            if (_audioFile == null || outputDevice == null)
            {
                MessageBox.Show("Please, select a song.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            outputDevice.Volume = 1f;
            outputDevice.Play();
            _isMusicPlaying = true;
        }
        public void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _timer.Stop();

            if (e.Exception != null)
            {
                _isMusicPlaying = false;
                MessageBox.Show($"Playback Error: {e.Exception.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
        // Called when the slider value is changed manually by the user
        public void SliderChanged()
        {
            if (_audioFile == null || outputDevice == null) return;

            _isSliderEnabled = true; // allow slider interaction
            SimpleLogger.Log($"Slider Value: {_startPage.MusicTrackBar.Value}, Maximum: {_startPage.MusicTrackBar.Maximum}");

            double frac = _viewModel.TrackPosition / 1000.0;
            TimeSpan currentTime = TimeSpan.FromSeconds(frac * _audioFile.TotalTime.TotalSeconds);
            _audioFile.CurrentTime = currentTime;
            _viewModel.TrackPosition = _startPage.MusicTrackBar.Value;  
            _viewModel.CurrentTime = currentTime.ToString(@"mm\:ss");
            SimpleLogger.Log($"Current time: {_viewModel.CurrentTime}");
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
                    _timer.Start();
                    _viewModel.GetStatusOnSlider = true;
                    _audioMetaService.TakeArtistSongName(CurrentMusicPath);
                    _audioMetaService.UpdateAlbumArt(CurrentMusicPath);
                    InitMusic(_currentMusicPath);
                    PlayMusic();
                    _newMusicPath = CurrentMusicPath;
                    isSliderEnabled = true;
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
                    _viewModel.PlayPauseButton = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\pause.png")));
                    _startPage.PlayButton.Width = 110;
                    _startPage.PlayButton.Margin = new Thickness(0, 0, 0, 0);
                    SimpleLogger.Log("Music paused");
                    _timer.Stop();
                    outputDevice.Pause();
                }
                else
                {
                    _viewModel.PlayPauseButton = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\play.png")));
                   _startPage.PlayButton.Width = 97;
                    _startPage.PlayButton.Margin = new Thickness(0, -5, 8, 0);
                    SimpleLogger.Log("Music play");
                    _timer.Start();
                    outputDevice.Play();
                }
                _isMusicPlaying = !_isMusicPlaying;
            }
        }

        // Stop and release resources for current audio
        public void StopAndDisposeCurrentMusic()
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

            isSliderEnabled = false;

            // Set default album image if not available
            var defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/menu/musicLogo.jpg");
            if (File.Exists(defaultImagePath))
            {
                SimpleLogger.Log("Set default icon logo for album art" + defaultImagePath);
                var image = new BitmapImage(new Uri(defaultImagePath, UriKind.Absolute));
                _viewModel.AlbumArt = image;
            }
            else
            {
                MessageBox.Show("Default image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);

            }
        }

        // Toggle "favorite" status for current song
        public void SelectFavoriteSongToPlayList(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null || _audioFile == null || _viewModel == null)
                return;

            _IsSelectedSongFavorite = !_IsSelectedSongFavorite;

            string path = _IsSelectedSongFavorite
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\favorite_b.png")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\sidebar\\favorite_a.png");

            if (!File.Exists(path))
            {
                MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);
                return;
            }

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                _viewModel.FavoriteSong = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load favorite image.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_IsSelectedSongFavorite)
            {
                if (_viewModel.Songs == null || _viewModel.Songs.Count == 0)
                {
                    MessageBox.Show("No songs available.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_viewModel.SelectedIndex < 0 || _viewModel.SelectedIndex >= _viewModel.Songs.Count)
                {
                    MessageBox.Show("Invalid song selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                var song = _viewModel.Songs[_viewModel.SelectedIndex];
                _viewModel.SongGrid.Add(new Song(song.Artist, song.Title, song.Duration));
                // Add the song to the playlist
                using (var writer = new BinaryWriter(File.Open(FileName, File.Exists(FileName) ? FileMode.Append : FileMode.Create)))
                {
                    writer.Write(song.Artist);
                    writer.Write(song.Title);
                    writer.Write(song.Duration);
                }
            }
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

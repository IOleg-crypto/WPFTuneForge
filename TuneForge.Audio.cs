using MaxMind.Db;
using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader _audioFile;
        private readonly DispatcherTimer _timer = new();
        private string _currentMusicPath;
        private string _newMusicPath;
        private bool _isMusicPlaying;
        private bool _isSoundOn;
        private bool _userIsDragging;
        private bool _IsSelectedSongFavorite;
        private ImageSource _albumArtImage;
        //Main class 
        private StartPage _startPage;

        public string CurrentMusicPath
        {
            get => _currentMusicPath ?? string.Empty;
            set => _currentMusicPath = value;
        }

        private void InitTimerMusic()
        {
            _startPage.MusicTrackBar.Value = 0;
            _startPage.MusicTrackBar.Maximum = 1000;
            _startPage.MusicTrackBar.Value = 0;

            _startPage.MusicTrackBar.PreviewMouseDown += (s, e) => _userIsDragging = true;
            _startPage.MusicTrackBar.PreviewMouseUp += MusicTrackBar_MouseUp;

            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += TimerTime_Tick;
        }

        private void InitDefaultAlbumArt()
        {
            _albumArtImage = _startPage.MusicLogo.Source;
        }

        private BitmapImage GetAlbumArt(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            using var tagFile = TagLib.File.Create(path);
            if (tagFile.Tag.Pictures.Length == 0)
            {
                return null;
            }

            var bin = tagFile.Tag.Pictures[0].Data.Data;
            if (bin.Length == 0)
            {
                return null;
            }

            using var ms = new MemoryStream(bin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        private void UpdateAlbumArt(string path)
        {
            var albumImage = GetAlbumArt(path);
            if (albumImage == null)
            {
                _startPage.MusicLogo.Source = _albumArtImage;
                return;
            }


            _startPage.MusicLogo.Source = albumImage;
        }

        private void TakeArtistSongName(string path)
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

                _startPage.NameArtist.Content = artist;
                _startPage.NameSong.Content = title;
            }
            catch
            {
                _startPage.NameArtist.Content = "";
                _startPage.NameSong.Content = "";
            }
        }

        private void InitMusic(string path)
        {
            _audioFile = new AudioFileReader(CurrentMusicPath);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(_audioFile);
            outputDevice.PlaybackStopped += OnPlaybackStopped;
            _isSoundOn = true;
        }
        private void PlayMusic()
        {
            if (_audioFile == null || outputDevice == null)
            {
                MessageBox.Show("No music device initialized.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            outputDevice.Volume = 1f;
            outputDevice.Play();
            _isMusicPlaying = true;
        }

        private void MusicTrackBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_audioFile == null) return;

            double frac = _startPage.MusicTrackBar.Value / _startPage.MusicTrackBar.Maximum;
            _audioFile.CurrentTime = TimeSpan.FromSeconds(frac * _audioFile.TotalTime.TotalSeconds);
            _startPage.StartMusicLabel.Content = _audioFile.CurrentTime.ToString(@"mm\:ss");
            _userIsDragging = false;
        }

        private void TimerTime_Tick(object sender, EventArgs e)
        {
            if (_audioFile == null || !_isMusicPlaying || _userIsDragging)
                return;

            double progress = _audioFile.CurrentTime.TotalSeconds / _audioFile.TotalTime.TotalSeconds;
            _startPage.MusicTrackBar.Value = progress * _startPage.MusicTrackBar.Maximum;

            _startPage.StartMusicLabel.Content = _audioFile.CurrentTime.ToString(@"mm\:ss");
            _startPage.EndMusicLabel.Content = _audioFile.TotalTime.ToString(@"mm\:ss");
        }

        private void ToggleSound(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null)
            {
                MessageBox.Show("No music device initialized.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _isSoundOn = !_isSoundOn;
            if(_isSoundOn)
            {
                outputDevice.Volume = 1f;
                var imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_new.png");
                _startPage.SoundButton.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            else
            {
                var imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_c.png");
                _startPage.SoundButton.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                outputDevice.Volume = 0f;
            }
        }

        private void OnClickMusic(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentMusicPath))
            {
                MessageBox.Show("No music selected", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
            _startPage.MusicTrackBar.Value = 0;
            _startPage.StartMusicLabel.Content = "00:00";
            _startPage.EndMusicLabel.Content = "00:00";

            // If song don`t have album art, set default
            BitmapImage image = new BitmapImage();
            image = _startPage.MusicLogo.Source as BitmapImage;
            _startPage.MusicLogo.Source = image;
        }

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

        private void StartMusic(object sender, RoutedEventArgs e)
        {
            if (_audioFile != null)
            {
                _audioFile.CurrentTime = TimeSpan.Zero;
                outputDevice?.Pause();
            }
        }

        private void EndMusic(object sender, RoutedEventArgs e)
        {
            if (_audioFile != null)
                _audioFile.CurrentTime = _audioFile.TotalTime - TimeSpan.FromMilliseconds(500);
        }

        private void SelectFavoriteSongToPlayList(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null || _audioFile == null) return;

            _IsSelectedSongFavorite = !_IsSelectedSongFavorite;
            string path = _IsSelectedSongFavorite
                ? System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\favorite_b.png")
                : System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\sidebar\\favorite_a.png");

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            _startPage.FavoriteSong.Source = bitmap;
        }

        private void RepeatSong(object sender, RoutedEventArgs e)
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

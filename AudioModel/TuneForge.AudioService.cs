using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.AudioModel
{
    public class AudioService
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader _audioFile;
        private TimerHelper _timer;
        private MusicViewModel _viewModel;
        private AudioMetaService _audioMetaService;
        private bool _isSoundOn;
        private bool _IsSelectedSongFavorite;
        private bool _isSliderEnabled = false;
       

        private StartPage _startPage = new();

        public bool isSliderEnabled { get => _isSliderEnabled; set => _isSliderEnabled = value; }
        public bool _isMusicPlaying;
        public bool _userIsDragging;

        private string _currentMusicPath;
        private string _newMusicPath;

        public WaveOutEvent _outputDevice { get => outputDevice; set => outputDevice = value; }
        public AudioFileReader audioFile { get => _audioFile; set => _audioFile = value; }
        public TimerHelper _timerHelper { get => _timer; set => _timer = value; }
        public StartPage startPage { get => _startPage; set => _startPage = value; }

        public AudioService(MusicViewModel viewModel)
        {
            _viewModel = viewModel;
            _audioMetaService = new AudioMetaService(_viewModel);
            _timer = new TimerHelper(TimeSpan.FromMilliseconds(700), this, _viewModel);
        }

        public string CurrentMusicPath
        {
            get => _currentMusicPath ?? string.Empty;
            set => _currentMusicPath = value;
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
                MessageBox.Show("Please, select a song.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            outputDevice.Volume = 1f;
            outputDevice.Play();
            _isMusicPlaying = true;
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
                    _audioMetaService.TakeArtistSongName(CurrentMusicPath);
                    InitMusic(_currentMusicPath);
                    PlayMusic();
                    _audioMetaService.UpdateAlbumArt(CurrentMusicPath);
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

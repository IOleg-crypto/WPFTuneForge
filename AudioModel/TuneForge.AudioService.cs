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
        private bool _isSoundOn;
        private bool _IsSelectedSongFavorite;
        private bool _isSliderEnabled = false;
        private readonly Random _random = new Random();
        private MMDeviceEnumerator enumerator;
        private StartPage _startPage = new();

        public bool isSliderEnabled { get => _isSliderEnabled; set => _isSliderEnabled = value; }
        public bool _isMusicPlaying;
        public bool isUserDragging = false;

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
            _timer = new TimerHelper(TimeSpan.FromMilliseconds(400), this, _viewModel);
            enumerator = new MMDeviceEnumerator();
        }

        public string CurrentMusicPath
        {
            get => _currentMusicPath ?? string.Empty;
            set => _currentMusicPath = value;
        }

        private void InitMusic(string path)
        {
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
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
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

            // Встановлюємо час відтворення
            _audioFile.CurrentTime = currentTime;

            // Оновлюємо ViewModel
            _viewModel.TrackPosition = _startPage.MusicTrackBar.Value;  // позиція слайдера (число)
            _viewModel.CurrentTime = currentTime.ToString(@"mm\:ss");
            SimpleLogger.Log($"Current time: {_viewModel.CurrentTime}");

            isUserDragging = false;
        }


        public void IncreaseSound(object sender, RoutedEventArgs e)
        {
            MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            float currentVolume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = currentVolume + 0.1f;
        }
        public void DecreaseSound(object sender, RoutedEventArgs e)
        {

            MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            float currentVolume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = currentVolume - 0.1f;
        }

        // Toggle mute/unmute
        public void ToggleSound(object sender, RoutedEventArgs e)
        {
            if (outputDevice == null) return;

            MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            bool currentlyMuted = device.AudioEndpointVolume.Mute;
            _isSoundOn = currentlyMuted; 

            if (_isSoundOn)
            {
                device.AudioEndpointVolume.Mute = false;

                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_new.png");
                SimpleLogger.Log("Changed icon: " + imagePath);
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                    return;
                }
                _viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            else
            {
                device.AudioEndpointVolume.Mute = true;

                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_c.png");
                SimpleLogger.Log("Changed icon: " + imagePath);
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                    return;
                }
                _viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }

            SimpleLogger.Log("System mute state: " + device.AudioEndpointVolume.Mute);
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
                    _viewModel.GetStatusOnSlider = true;
                    _audioMetaService.TakeArtistSongName(CurrentMusicPath);
                    InitMusic(_currentMusicPath);
                    PlayMusic();
                    _audioMetaService.UpdateAlbumArt(CurrentMusicPath);
                    _timer.Start();
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
                    SimpleLogger.Log("Music paused");
                    _timer.Stop();
                    outputDevice.Pause();
                }
                else
                {
                    SimpleLogger.Log("Music play");
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

            if (!File.Exists(path))
            {
                MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);
                return;
            }

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
        public void ChaoticPlaySong(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            isSliderEnabled = true;

            int index = _random.Next(_viewModel.Songs.Count);
            CurrentMusicPath = _viewModel.Songs[index].FilePath;
            SimpleLogger.Log($"Playing music: {CurrentMusicPath}");

            StopAndDisposeCurrentMusic();

            try
            {
                _audioMetaService.TakeArtistSongName(CurrentMusicPath);
                _audioMetaService?.UpdateAlbumArt(CurrentMusicPath);

                _audioFile = new AudioFileReader(CurrentMusicPath);

                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }


                outputDevice.Init(_audioFile);
                outputDevice.Play();

                _newMusicPath = CurrentMusicPath;
                _isMusicPlaying = true;
                _timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
     
}

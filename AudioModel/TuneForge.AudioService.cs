﻿using NAudio.CoreAudioApi;
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
        // Private fields
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private TimerHelper timer;
        private MusicViewModel viewModel;
        private AudioMetaService audioMetaService;
        private StartPage startPage = new();
        private VolumeService volumeService;
        private MusicNavigationService musicNavigationService;
        private DeviceOutputModel deviceOutputModel;

        private bool isSoundOn;
        private bool isSelectedSongFavorite = false;
        private bool isSliderEnabled = false;
        private bool isMusicPlaying;
        private string currentMusicPath;
        private string newMusicPath;
        private string FileName = "FavoriteSong.bin";
        private bool isManualStop = false; // to avoid false error message on manual Stop()

        // Public properties
        public WaveOutEvent OutputDevice { get => outputDevice; set => outputDevice = value; }
        public AudioFileReader AudioFile { get => audioFile; set => audioFile = value; }
        public TimerHelper TimerHelper { get => timer; set => timer = value; }
        public StartPage StartPage { get => startPage; set => startPage = value; }
        public VolumeService VolumeService { get => volumeService; set => volumeService = value; }
        public MusicNavigationService MusicNavigationService { get => musicNavigationService; set => musicNavigationService = value; }
        public MusicViewModel MusicViewModel { get => viewModel; set => viewModel = value; }
        public DeviceOutputModel DeviceOutputModel { get => deviceOutputModel; set => deviceOutputModel = value; }

        public bool IsSound { get => isSoundOn; set => isSoundOn = value; }
        public bool IsSelectedSongFavorite { get => isSelectedSongFavorite; set => isSelectedSongFavorite = value; }
        public bool IsSliderEnabled { get => isSliderEnabled; set => isSliderEnabled = value; }
        public bool IsMusicPlaying { get => isMusicPlaying; set => isMusicPlaying = value; }
        public string CurrentMusicPath { get => currentMusicPath ?? string.Empty; set => currentMusicPath = value; }
        public string NewMusicPath { get => newMusicPath ?? string.Empty; set => newMusicPath = value; }
        public bool IsManualStop { get => isManualStop; set => isManualStop = value; }

        // Constructor
        public AudioService(MusicViewModel viewModel)
        {
            this.viewModel = viewModel;
            audioMetaService = new AudioMetaService(viewModel);
            volumeService = new VolumeService(this, viewModel);
            musicNavigationService = new MusicNavigationService(viewModel, this, audioMetaService);
            timer = new TimerHelper(TimeSpan.FromMilliseconds(400), this, viewModel);
        }

        // Initialize audio objects
        private void InitMusic(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            SimpleLogger.Log("Init music - AudioFileReader and WaveOutEvent");
            AudioFile = new AudioFileReader(CurrentMusicPath);
            OutputDevice = new WaveOutEvent();
            OutputDevice.Init(AudioFile);
            OutputDevice.PlaybackStopped += OnPlaybackStopped;
            IsSound = true;
        }

        // Play loaded music
        private void PlayMusic()
        {
            SimpleLogger.Log("Play music");

            if (AudioFile == null || OutputDevice == null)
            {
                MessageBox.Show("Please, select a song.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OutputDevice.Volume = 1f;
            OutputDevice.Play();
            IsMusicPlaying = true;
        }

        // Handle playback stop (either end of track or manual stop)
        public void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            TimerHelper.Stop();

            // Show error only if not manually stopped
            if (e.Exception != null && !isManualStop)
            {
                IsMusicPlaying = false;
                MessageBox.Show($"Playback Error: {e.Exception.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsManualStop = false;

            // Automatic playback (next song)
            if (viewModel.DeviceOutputModel.IsAutomaticPlayback)
            {
                MusicNavigationService.EndMusic(this, null);
            }
            else if (IsMusicPlaying && AudioFile != null && OutputDevice != null)
            {
                TimerHelper.Start();
                OutputDevice.Play();
                IsMusicPlaying = true;
            }
            else
            {
                IsMusicPlaying = false;
            }
        }

        // Update music position via slider
        public void SliderChanged()
        {
            if (AudioFile == null || OutputDevice == null) return;

            IsSliderEnabled = true;
            SimpleLogger.Log($"Slider Value: {StartPage.MusicTrackBar.Value}, Maximum: {StartPage.MusicTrackBar.Maximum}");

            double frac = MusicViewModel.TrackPosition / 1000.0;
            TimeSpan currentTime = TimeSpan.FromSeconds(frac * AudioFile.TotalTime.TotalSeconds);
            AudioFile.CurrentTime = currentTime;
            MusicViewModel.TrackPosition = StartPage.MusicTrackBar.Value;
            MusicViewModel.CurrentTime = currentTime.ToString(@"mm\:ss");

            SimpleLogger.Log($"Current time: {MusicViewModel.CurrentTime}");
        }

        // User clicks to play or pause music
        public void OnClickMusic(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentMusicPath))
            {
                MessageBox.Show("No music selected", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // If the song changed, stop previous one
            if (NewMusicPath != CurrentMusicPath)
                StopAndDisposeCurrentMusic();

            if (OutputDevice == null || AudioFile == null)
            {
                try
                {
                    TimerHelper.Start();
                    MusicViewModel.GetStatusOnSlider = true;
                    audioMetaService.TakeArtistSongName(CurrentMusicPath);
                    audioMetaService.UpdateAlbumArt(CurrentMusicPath);
                    InitMusic(CurrentMusicPath);
                    PlayMusic();
                    NewMusicPath = CurrentMusicPath;
                    IsSliderEnabled = true;

                    // Update UI Play/Pause icon
                    MusicViewModel.PlayPauseButton = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\pause.png")));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error playing audio: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Toggle play/pause
                if (IsMusicPlaying)
                {
                    MusicViewModel.PlayPauseButton = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\play.png")));
                    SimpleLogger.Log("Music paused");
                    TimerHelper.Stop();
                    OutputDevice.Pause();
                }
                else
                {
                    MusicViewModel.PlayPauseButton = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\pause.png")));
                    TimerHelper.Start();
                    OutputDevice.Play();
                }
                IsMusicPlaying = !IsMusicPlaying;
            }
        }

        // Stop playback and release all resources
        public void StopAndDisposeCurrentMusic()
        {
            TimerHelper.Stop();
            IsManualStop = true;

            if (OutputDevice != null)
            {
                OutputDevice.PlaybackStopped -= OnPlaybackStopped;
                OutputDevice.Stop();
                OutputDevice.Dispose();
                OutputDevice = null;
            }

            AudioFile?.Dispose();
            AudioFile = null;

            IsMusicPlaying = false;
            MusicViewModel.TrackPosition = 0;
            MusicViewModel.CurrentTime = "00:00";
            MusicViewModel.EndTime = "00:00";
            IsSliderEnabled = false;
            IsSelectedSongFavorite = false;

            // Reset favorite icon
            var defaultFavoriteIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\sidebar\\favorite_a.png");
            if (File.Exists(defaultFavoriteIconPath))
            {
                MusicViewModel.FavoriteSong = new BitmapImage(new Uri(defaultFavoriteIconPath, UriKind.Absolute));
            }
            else
            {
                MessageBox.Show("Default favorite icon not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Reset album art
            var defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/menu/musicLogo.jpg");
            if (File.Exists(defaultImagePath))
            {
                SimpleLogger.Log("Set default album art: " + defaultImagePath);
                MusicViewModel.AlbumArt = new BitmapImage(new Uri(defaultImagePath, UriKind.Absolute));
            }
            else
            {
                MessageBox.Show("Default image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);
            }
        }

        // Toggle favorite status for current song
        public void SelectFavoriteSongToPlayList(object sender, RoutedEventArgs e)
        {
            if (OutputDevice == null || AudioFile == null || MusicViewModel == null)
                return;

            IsSelectedSongFavorite = !IsSelectedSongFavorite;

            string path = IsSelectedSongFavorite
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
                MusicViewModel.FavoriteSong = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load favorite image.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsSelectedSongFavorite)
            {
                if (MusicViewModel.Songs == null || MusicViewModel.Songs.Count == 0)
                {
                    MessageBox.Show("No songs available.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (MusicViewModel.SelectedIndex < 0 || MusicViewModel.SelectedIndex >= MusicViewModel.Songs.Count)
                {
                    MessageBox.Show("Invalid song selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var song = MusicViewModel.Songs[MusicViewModel.SelectedIndex];
                MusicViewModel.SongGrid.Add(new Song(song.Artist, song.Title, song.Duration));

                using (var writer = new BinaryWriter(File.Open(FileName, File.Exists(FileName) ? FileMode.Append : FileMode.Create)))
                {
                    writer.Write(song.Artist);
                    writer.Write(song.Title);
                    writer.Write(song.Duration);
                }
            }
        }

        // Restart current song from beginning
        public void RepeatSong(object sender, RoutedEventArgs e)
        {
            if (AudioFile == null || OutputDevice == null)
                return;

            AudioFile.Position = 0;
            TimerHelper.Start();
            OutputDevice.Play();
            IsMusicPlaying = true;
        }
    }
}

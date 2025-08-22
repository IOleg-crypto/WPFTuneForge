using NAudio.Wave;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.AudioModel
{
    public class MusicNavigationService
    {
        private AudioService audioService;
        private AudioMetaService audioMetaService;
        private MusicViewModel viewModel;
        private Random _random = new Random();

        public MusicNavigationService(MusicViewModel viewModel, AudioService audioService, AudioMetaService audioMetaService)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.audioMetaService = audioMetaService;
        }

        public AudioService AudioService
        {
            get => audioService;
            set => audioService = value;
        }

        public AudioMetaService AudioMetaService
        {
            get => audioMetaService;
            set => audioMetaService = value;
        }

        public MusicViewModel ViewModel
        {
            get => viewModel;
            set => viewModel = value;
        }

        public Random Random
        {
            get => _random;
            set => _random = value;
        }

        /// <summary>
        /// Move to the previous song and play it.
        /// </summary>
        public void StartMusic(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (AudioService.AudioFile != null)
            {
                AudioService.AudioFile.CurrentTime = TimeSpan.Zero;
                AudioService.OutputDevice?.Pause();
            }

            AudioService.IsSliderEnabled = true;
            AudioService.IsSelectedSongFavorite = false;

            int updatedIndex = ViewModel.SelectedIndex - 1;
            if (updatedIndex < 0)
                updatedIndex = ViewModel.Songs.Count - 1; // Wrap around to the last song

            ViewModel.SelectedIndex = updatedIndex;
            AudioService.CurrentMusicPath = ViewModel.Songs[updatedIndex].FilePath;
            SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");

            AudioService.StopAndDisposeCurrentMusic();

            try
            {
                AudioMetaService.TakeArtistSongName(AudioService.CurrentMusicPath);
                AudioMetaService.UpdateAlbumArt(AudioService.CurrentMusicPath);

                AudioService.AudioFile = new AudioFileReader(AudioService.CurrentMusicPath);

                if (AudioService.OutputDevice == null)
                {
                    AudioService.OutputDevice = new WaveOutEvent();
                    AudioService.OutputDevice.PlaybackStopped += AudioService.OnPlaybackStopped;
                }

                AudioService.OutputDevice.Init(AudioService.AudioFile);
                AudioService.OutputDevice.Play();

                AudioService.NewMusicPath = AudioService.CurrentMusicPath;
                AudioService.IsMusicPlaying = true;
                AudioService.TimerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Move to the next song and play it.
        /// </summary>
        public void EndMusic(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AudioService.IsSliderEnabled = true;

            int updatedIndex = ViewModel.SelectedIndex + 1;
            if (updatedIndex >= ViewModel.Songs.Count)
            {
                SimpleLogger.Log("Reached the end of the playlist.");
                updatedIndex = 0; // Wrap to beginning
            }

            ViewModel.SelectedIndex = updatedIndex;
            AudioService.CurrentMusicPath = ViewModel.Songs[updatedIndex].FilePath;
            SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");

            AudioService.StopAndDisposeCurrentMusic();

            try
            {
                AudioMetaService.TakeArtistSongName(AudioService.CurrentMusicPath);
                AudioMetaService.UpdateAlbumArt(AudioService.CurrentMusicPath);

                AudioService.AudioFile = new AudioFileReader(AudioService.CurrentMusicPath);

                AudioService.OutputDevice?.Dispose();
                AudioService.OutputDevice = new WaveOutEvent();
                AudioService.OutputDevice.PlaybackStopped += AudioService.OnPlaybackStopped;

                AudioService.OutputDevice.Init(AudioService.AudioFile);
                AudioService.OutputDevice.Play();

                AudioService.NewMusicPath = AudioService.CurrentMusicPath;
                AudioService.IsMusicPlaying = true;
                AudioService.TimerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Randomly pick a song from the playlist and play it.
        /// </summary>
        public void ChaoticPlaySong(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AudioService.IsSelectedSongFavorite = false;
            AudioService.IsSliderEnabled = true;

            int index = Random.Next(ViewModel.Songs.Count);
            AudioService.CurrentMusicPath = ViewModel.Songs[index].FilePath;
            SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");

            AudioService.StopAndDisposeCurrentMusic();

            try
            {
                AudioMetaService.TakeArtistSongName(AudioService.CurrentMusicPath);
                AudioMetaService.UpdateAlbumArt(AudioService.CurrentMusicPath);

                AudioService.AudioFile = new AudioFileReader(AudioService.CurrentMusicPath);

                if (AudioService.OutputDevice == null)
                {
                    AudioService.OutputDevice = new WaveOutEvent();
                    AudioService.OutputDevice.PlaybackStopped += AudioService.OnPlaybackStopped;
                }

                AudioService.OutputDevice.Init(AudioService.AudioFile);
                AudioService.OutputDevice.Play();

                AudioService.NewMusicPath = AudioService.CurrentMusicPath;
                AudioService.IsMusicPlaying = true;
                AudioService.TimerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

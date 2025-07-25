using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TagLib.Mpeg;
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
        public Random Random
        {
            get { return _random; }
            set { _random = value; }
        }
        public MusicNavigationService(MusicViewModel viewModel , AudioService audioService , AudioMetaService audioMetaService)
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

        // Set playback to start (0 sec) and shift to previous song
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
            // Needed to reset favorite icon
            AudioService.IsSelectedSongFavorite = false;

            int updatedIndex = ViewModel.SelectedIndex - 1;
            if (updatedIndex < 0)
            {
                updatedIndex = ViewModel.Songs.Count; // return to end
            }

            if (updatedIndex < ViewModel.Songs.Count)
            {
                ViewModel.SelectedIndex = updatedIndex;
                AudioService.CurrentMusicPath = ViewModel.Songs[updatedIndex].FilePath;
                SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");
            }
            else
            {
                SimpleLogger.Log("Reached the end of the playlist.");

                SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");
            }
            AudioService.StopAndDisposeCurrentMusic();

            try
            {
                AudioMetaService.TakeArtistSongName(AudioService.CurrentMusicPath);
                AudioMetaService?.UpdateAlbumArt(AudioService.CurrentMusicPath);

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
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // Set playback to end (almost finish)
        public void EndMusic(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AudioService.IsSliderEnabled = true;
            AudioService.IsSelectedSongFavorite = true;

            int updatedIndex = ViewModel.SelectedIndex + 1;

            if (updatedIndex >= ViewModel.Songs.Count)
            {
                SimpleLogger.Log("Reached the end of the playlist.");
                updatedIndex = 0;
            }

            ViewModel.SelectedIndex = updatedIndex;
            AudioService.CurrentMusicPath = ViewModel.Songs[updatedIndex].FilePath;
            SimpleLogger.Log($"Playing music: {AudioService.CurrentMusicPath}");

            AudioService.StopAndDisposeCurrentMusic();

            try
            {
                AudioMetaService.TakeArtistSongName(AudioService.CurrentMusicPath);
                AudioMetaService?.UpdateAlbumArt(AudioService.CurrentMusicPath);

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
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
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
                AudioMetaService?.UpdateAlbumArt(AudioService.CurrentMusicPath);

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
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

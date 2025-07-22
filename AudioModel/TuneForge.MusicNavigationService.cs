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
        private readonly Random _random = new Random();

        public MusicNavigationService(MusicViewModel viewModel , AudioService audioService , AudioMetaService audioMetaService)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.audioMetaService = audioMetaService;
        }
        // Set playback to start (0 sec) and shift to previous song
        public void StartMusic(object sender, RoutedEventArgs e)
        {
            if (viewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (audioService.audioFile != null)
            {
                audioService.audioFile.CurrentTime = TimeSpan.Zero;
                audioService._outputDevice?.Pause();
            }
            audioService.isSliderEnabled = true;

            int updatedIndex = viewModel.SelectedIndex - 1;
            if (updatedIndex < 0)
            {
                updatedIndex = viewModel.Songs.Count; // return to end
            }

            if (updatedIndex < viewModel.Songs.Count)
            {
                viewModel.SelectedIndex = updatedIndex;
                audioService.CurrentMusicPath = viewModel.Songs[updatedIndex].FilePath;
                SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
            }
            else
            {
                SimpleLogger.Log("Reached the end of the playlist.");

                SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
            }
            audioService.StopAndDisposeCurrentMusic();

            try
            {
                audioMetaService.TakeArtistSongName(audioService.CurrentMusicPath);
                audioMetaService?.UpdateAlbumArt(audioService.CurrentMusicPath);

                audioService.audioFile = new AudioFileReader(audioService.CurrentMusicPath);

                if (audioService._outputDevice == null)
                {
                    audioService._outputDevice = new WaveOutEvent();
                    audioService._outputDevice.PlaybackStopped += audioService.OnPlaybackStopped;
                }


                audioService._outputDevice.Init(audioService.audioFile);
                audioService._outputDevice.Play();

                audioService.NewMusicPath = audioService.CurrentMusicPath;
                audioService._isMusicPlaying = true;
                audioService._timerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // Set playback to end (almost finish)
        public void EndMusic(object sender, RoutedEventArgs e)
        {
            if (viewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (audioService.audioFile != null)
            {
                audioService.audioFile.CurrentTime = audioService.audioFile.TotalTime - TimeSpan.FromMilliseconds(500);
            }
            int updatedIndex = viewModel.SelectedIndex + 1;

            if (updatedIndex < viewModel.Songs.Count)
            {
                viewModel.SelectedIndex = updatedIndex;
                audioService.CurrentMusicPath = viewModel.Songs[updatedIndex].FilePath;
                SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
            }
            else
            {
                SimpleLogger.Log("Reached the end of the playlist.");
                updatedIndex = 0;
                viewModel.SelectedIndex = updatedIndex;
                audioService.CurrentMusicPath = viewModel.Songs[updatedIndex].FilePath;
                SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
            }

            audioService.StopAndDisposeCurrentMusic();

            try
            {
                audioMetaService.TakeArtistSongName(audioService.CurrentMusicPath);
                audioMetaService?.UpdateAlbumArt(audioService.CurrentMusicPath);

                audioService.audioFile = new AudioFileReader(audioService.CurrentMusicPath);

                if (audioService._outputDevice == null)
                {
                    audioService._outputDevice = new WaveOutEvent();
                    audioService._outputDevice.PlaybackStopped += audioService.OnPlaybackStopped;
                }


                audioService._outputDevice.Init(audioService.audioFile);
                audioService._outputDevice.Play();

                audioService.NewMusicPath = audioService.CurrentMusicPath;
                audioService._isMusicPlaying = true;
                audioService._timerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void ChaoticPlaySong(object sender, RoutedEventArgs e)
        {
            if (viewModel.Songs.Count == 0)
            {
                MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            audioService.isSliderEnabled = true;

            int index = _random.Next(viewModel.Songs.Count);
            audioService.CurrentMusicPath = viewModel.Songs[index].FilePath;
            SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");

            audioService.StopAndDisposeCurrentMusic();

            try
            {
                audioMetaService.TakeArtistSongName(audioService.CurrentMusicPath);
                audioMetaService?.UpdateAlbumArt(audioService.CurrentMusicPath);

                audioService.audioFile = new AudioFileReader(audioService.CurrentMusicPath);

                if (audioService._outputDevice == null)
                {
                    audioService._outputDevice = new WaveOutEvent();
                    audioService._outputDevice.PlaybackStopped += audioService.OnPlaybackStopped;
                }


                audioService._outputDevice.Init(audioService.audioFile);
                audioService._outputDevice.Play();

                audioService.NewMusicPath = audioService.CurrentMusicPath;
                audioService._isMusicPlaying = true;
                audioService._timerHelper.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Model;

namespace WpfTuneForgePlayer.ViewModel
{
    public class BindingCommands
    {
        // Commands for various UI actions
        public ICommand PlayCommand { get; set; }
        public ICommand SelectFavoriteSong { get; set; }
        public ICommand ToggleSound { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand StartMusic { get; set; }
        public ICommand EndMusic { get; set; }
        public ICommand ToggleAudio { get; set; }
        public ICommand ChangeMusicTime { get; set; }
        public ICommand ReloadMusicPage { get; set; }
        public ICommand TakeTimer { get; set; }
        public ICommand SelectChaoticallySong { get; set; }
        public ICommand PlaySelectedSongCommand { get; set; }
        public ICommand IncreaseVolume { get; set; }
        public ICommand DecreaseVolume { get; set; }

        // Initialize commands by binding them to methods in services and view model
        public void InitCommands(MusicViewModel viewModel, AudioService audioService, AudioMetaService audioMetaService)
        {
            // Play or pause music command
            PlayCommand = new RelayCommand(() => audioService.OnClickMusic(viewModel, null));

            // Toggle favorite song in playlist
            SelectFavoriteSong = new RelayCommand(() => audioService.SelectFavoriteSongToPlayList(viewModel, null));

            // Repeat current song
            RepeatCommand = new RelayCommand(() => audioService.RepeatSong(viewModel, null));

            // Toggle mute/unmute sound
            ToggleAudio = new RelayCommand(() => audioService.VolumeService.ToggleSound());

            // Play next song in playlist
            EndMusic = new RelayCommand(() => audioService.MusicNavigationService.EndMusic(viewModel, null));

            // Change song playback position via slider
            ChangeMusicTime = new RelayCommand(() => audioService.SliderChanged());

            // Reload the music list/page
            ReloadMusicPage = new RelayCommand(() => viewModel.LoadSongs(viewModel.TakeCurrentDirectory));

            // Update timer manually (used for UI update)
            TakeTimer = new RelayCommand(() => audioService.TimerHelper?.TimerTime_Tick(viewModel, null));

            // Play a random song
            SelectChaoticallySong = new RelayCommand(() => audioService.MusicNavigationService.ChaoticPlaySong(viewModel, null));

            // Play previous song
            StartMusic = new RelayCommand(() => audioService.MusicNavigationService.StartMusic(viewModel, null));

            // Play a specific song selected from the list
            PlaySelectedSongCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<SongModel>(song =>
            {
                if (song != null)
                {
                    audioService.CurrentMusicPath = song.FilePath;
                    audioMetaService.TakeArtistSongName(song.FilePath);
                    audioMetaService.UpdateAlbumArt(song.FilePath);

                    // Update selected index in the view model
                    int index = viewModel.Songs.IndexOf(song);
                    viewModel.SelectedIndex = index;
                }
            });

            // Increase system volume
            IncreaseVolume = new RelayCommand(() => audioService.VolumeService.IncreaseVolume());

            // Decrease system volume
            DecreaseVolume = new RelayCommand(() => audioService.VolumeService.DecreaseVolume());
        }
    }
}

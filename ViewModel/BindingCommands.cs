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


 
        public void InitCommands(MusicViewModel viewModel , AudioService audioService, AudioMetaService audioMetaService)
        {
            PlayCommand = new RelayCommand(() => audioService.OnClickMusic(viewModel, null));
            SelectFavoriteSong = new RelayCommand(() => audioService.SelectFavoriteSongToPlayList(viewModel, null));
            RepeatCommand = new RelayCommand(() => audioService.RepeatSong(viewModel, null));
            ToggleAudio = new RelayCommand(() => audioService.VolumeService.ToggleSound());
            EndMusic = new RelayCommand(() => audioService.MusicNavigationService.EndMusic(viewModel, null));
            ChangeMusicTime = new RelayCommand(() => audioService.SliderChanged());
            ReloadMusicPage = new RelayCommand(() => viewModel.LoadSongs(viewModel.TakeCurrentDirectory));
            TakeTimer = new RelayCommand(() => audioService.TimerHelper?.TimerTime_Tick(viewModel, null));
            SelectChaoticallySong = new RelayCommand(() => audioService.MusicNavigationService.ChaoticPlaySong(viewModel, null));
            StartMusic = new RelayCommand(() => audioService.MusicNavigationService.StartMusic(viewModel, null));
            PlaySelectedSongCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<SongModel>(song =>
            {
                if (song != null)
                {
                    audioService.CurrentMusicPath = song.FilePath;
                    audioMetaService.TakeArtistSongName(song.FilePath);
                    audioMetaService.UpdateAlbumArt(song.FilePath);
                    // Get index of the selected song in the list of songs
                    int index = viewModel.Songs.IndexOf(song);
                    viewModel.SelectedIndex = index;
                }
            }); 

            IncreaseVolume = new RelayCommand(() => audioService.VolumeService.IncreaseVolume());
            DecreaseVolume = new RelayCommand(() => audioService.VolumeService.DecreaseVolume());

        }
    }
}

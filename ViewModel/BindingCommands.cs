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


 
        public void InitCommands(MusicViewModel viewModel , AudioService audioService, AudioMetaService audioMetaService)
        {
            PlayCommand = new RelayCommand(() => audioService.OnClickMusic(viewModel, null));
            SelectFavoriteSong = new RelayCommand(() => audioService.SelectFavoriteSongToPlayList(viewModel, null));
            RepeatCommand = new RelayCommand(() => audioService.RepeatSong(viewModel, null));
            ToggleAudio = new RelayCommand(() => audioService.ToggleSound(viewModel, null));
            EndMusic = new RelayCommand(() => audioService.EndMusic(viewModel, null));
            ChangeMusicTime = new RelayCommand(() => audioService.SliderChanged());
            ReloadMusicPage = new RelayCommand(() => viewModel.LoadSongs(viewModel.TakeCurrentDirectory));
            TakeTimer = new RelayCommand(() => audioService._timerHelper?.TimerTime_Tick(viewModel, null));
            SelectChaoticallySong = new RelayCommand(() => audioService.ChaoticPlaySong(viewModel, null));
            StartMusic = new RelayCommand(() => audioService.StartMusic(viewModel, null));
            PlaySelectedSongCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<SongModel>(song =>
            {
                if (song != null)
                {
                    audioService.CurrentMusicPath = song.FilePath;
                    audioMetaService.TakeArtistSongName(song.FilePath);
                    audioMetaService.UpdateAlbumArt(song.FilePath);
                }
            }); 

        }
    }
}

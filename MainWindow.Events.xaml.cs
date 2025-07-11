using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        private MusicViewModel _viewModel = new();
        private void OnMusicSelected(string path)
        {
            CurrentMusicPath = path;
            TakeArtistSongName(path);
            UpdateAlbumArt(path);
        }

        private void ActionHandle()
        {
            Sidebar.MusicSelected += OnMusicSelected;
        }

        private void InitMusicDirectory()
        {
            _startPage = new StartPage();
            _startPage.DataContext = _viewModel;
            _viewModel.MainWindow = this;
            MainContentFrame.Navigate(_startPage);


            Sidebar.ShowMusicDirectory += OnShowMusicDirectory;
        }
        private void OnShowMusicDirectory(object sender, EventArgs e)
        {
            MainContentFrame.Navigate(new MusicDirectory());
        }
    }
}
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
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;
using WpfTuneForgePlayer.Views;

namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        private MusicViewModel _viewModel;
        private DeviceOutputModel _deviceOutputModel;
        private AudioService audioService;
        private AudioMetaService audioMetaService;
        private FavoriteSongs favoriteSongs;

        public FavoriteSongs _favoriteSongs { get => favoriteSongs; set=> favoriteSongs = value; }


        private void OnMusicSelected(string path)
        {
            audioService.CurrentMusicPath = path;
            audioMetaService.TakeArtistSongName(path);
            audioMetaService.UpdateAlbumArt(path);
        }

        private void ActionHandle()
        {
            Sidebar.MusicSelected += OnMusicSelected;
            Sidebar.NavigateToSettings += OnNavigateToSettings;
            Sidebar.ShowMusicDirectory += OnShowMusicDirectory;
            Sidebar.FavoritePage += NavigateToFavoritePage;
            
        }

        private void OnShowMusicDirectory(object sender, EventArgs e)
        {
            MainContentFrame.Navigate(new MusicDirectory(_viewModel));
        }

        private void OnNavigateToSettings(object sender, EventArgs e)
        {
            var settingsPage = new Settings(_deviceOutputModel);
            settingsPage.backToStartPage += (_, __) => NavigateToStartPage();
            MainContentFrame.Navigate(settingsPage);
        }

        private void NavigateToStartPage()
        {
            var startPage = new StartPage
            {
                DataContext = _viewModel
            };
            MainContentFrame.Navigate(startPage);
        }

        private void NavigateToFavoritePage(object sender, EventArgs e)
        {
            var favoriteSongs = new FavoriteSongs(_viewModel)
            {
                DataContext = _viewModel
            };
            MainContentFrame.Navigate(favoriteSongs);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ExternalConsoleLogger.StopConsoleWatcher();
            Close();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        
    }
}
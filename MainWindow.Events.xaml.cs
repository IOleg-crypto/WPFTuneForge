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
        private AudioService _audioService;
        private AudioMetaService _audioMetaService;
        private FavoriteSongs _favoriteSongs;

        public MusicViewModel ViewModel
        {
            get => _viewModel;
            set => _viewModel = value;
        }

        public DeviceOutputModel DeviceOutputModel
        {
            get => _deviceOutputModel;
            set => _deviceOutputModel = value;
        }

        public AudioService AudioService
        {
            get => _audioService;
            set => _audioService = value;
        }

        public AudioMetaService AudioMetaService
        {
            get => _audioMetaService;
            set => _audioMetaService = value;
        }

        public FavoriteSongs FavoriteSongs
        {
            get => _favoriteSongs;
            set => _favoriteSongs = value;
        }


        private void OnMusicSelected(string path)
        {
            AudioService.CurrentMusicPath = path;
            AudioMetaService.TakeArtistSongName(path);
            AudioMetaService.UpdateAlbumArt(path);
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
            MainContentFrame.Navigate(new MusicDirectory(ViewModel));
        }

        private void OnNavigateToSettings(object sender, EventArgs e)
        {
            var settingsPage = new Settings(DeviceOutputModel);
            settingsPage.backToStartPage += (_, __) => NavigateToStartPage();
            MainContentFrame.Navigate(settingsPage);
        }

        private void NavigateToStartPage()
        {
            var startPage = new StartPage
            {
                DataContext = ViewModel
            };
            MainContentFrame.Navigate(startPage);
        }

        private void NavigateToFavoritePage(object sender, EventArgs e)
        {
            var favoriteSongs = new FavoriteSongs(ViewModel)
            {
                DataContext = ViewModel
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
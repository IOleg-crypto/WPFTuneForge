using System;
using System.Windows;
using System.Windows.Input;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;
using WpfTuneForgePlayer.Views;

namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        // Private fields for core components and services
        private MusicViewModel _viewModel;
        private DeviceOutputModel _deviceOutputModel;
        private AudioService _audioService;
        private AudioMetaService _audioMetaService;
        private FavoriteSongs _favoriteSongs;

        // Properties for accessing the fields
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

        /// <summary>
        /// Handles the event when a music track is selected.
        /// Updates the current music path and metadata.
        /// </summary>
        private void OnMusicSelected(string path)
        {
            AudioService.CurrentMusicPath = path;
            AudioMetaService.TakeArtistSongName(path);
            AudioMetaService.UpdateAlbumArt(path);
        }

        /// <summary>
        /// Attaches event handlers to sidebar actions.
        /// </summary>
        private void ActionHandle()
        {
            Sidebar.MusicSelected += OnMusicSelected;
            Sidebar.NavigateToSettings += OnNavigateToSettings;
            Sidebar.ShowMusicDirectory += OnShowMusicDirectory;
            Sidebar.FavoritePage += NavigateToFavoritePage;
        }

        /// <summary>
        /// Navigates to the music directory page.
        /// </summary>
        private void OnShowMusicDirectory(object sender, EventArgs e)
        {
            MainContentFrame.Navigate(new MusicDirectory(ViewModel));
        }

        /// <summary>
        /// Navigates to the settings page, with callback to return to start page.
        /// </summary>
        private void OnNavigateToSettings(object sender, EventArgs e)
        {
            var settingsPage = new Settings(DeviceOutputModel);
            settingsPage.backToStartPage += (_, __) => NavigateToStartPage();
            MainContentFrame.Navigate(settingsPage);
        }

        /// <summary>
        /// Navigates to the start page and binds ViewModel as DataContext.
        /// </summary>
        private void NavigateToStartPage()
        {
            var startPage = new StartPage
            {
                DataContext = ViewModel
            };
            MainContentFrame.Navigate(startPage);
        }

        /// <summary>
        /// Navigates to the favorite songs page and binds ViewModel as DataContext.
        /// </summary>
        private void NavigateToFavoritePage(object sender, EventArgs e)
        {
            var favoriteSongs = new FavoriteSongs(ViewModel)
            {
                DataContext = ViewModel
            };
            MainContentFrame.Navigate(favoriteSongs);
        }

        /// <summary>
        /// Minimizes the window when minimize button is clicked.
        /// </summary>
        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        /// <summary>
        /// Stops the external console logger and closes the window on close button click.
        /// </summary>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ExternalConsoleLogger.StopConsoleWatcher();
            Close();
        }

        /// <summary>
        /// Enables window dragging by mouse left button hold.
        /// </summary>
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}

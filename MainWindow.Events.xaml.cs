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
using WpfTuneForgePlayer.Views;

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
            Sidebar.NavigateToSettings += OnNavigateToSettings;
            //Sidebar.NavigateToStartPage += (_, __) => NavigateToStartPage();
            Sidebar.ShowMusicDirectory += OnShowMusicDirectory;
        }

        private void InitMusicDirectory()
        {
            NavigateToStartPage();
            _viewModel.MainWindow = this;
        }

        private void OnShowMusicDirectory(object sender, EventArgs e)
        {
            MainContentFrame.Navigate(new MusicDirectory(_viewModel));
        }

        private void OnNavigateToSettings(object sender, EventArgs e)
        {
            var settingsPage = new Settings();
            settingsPage.DataContext = _viewModel;

            settingsPage.backToStartPage += (_, __) => NavigateToStartPage();

            MainContentFrame.Navigate(settingsPage);
        }

        private void NavigateToStartPage()
        {
            var startPage = new StartPage();
            startPage.DataContext = _viewModel;
            MainContentFrame.Navigate(startPage);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
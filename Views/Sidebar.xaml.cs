using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfTuneForgePlayer.Views;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer
{
    public partial class Sidebar : UserControl
    {
        public event Action<string> MusicSelected;
        public event EventHandler ShowMusicDirectory;
        private MainWindow _mainWindow;
        private MusicViewModel _viewModel = new MusicViewModel();

        public Sidebar()
        {
            InitializeComponent();
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Favorite clicked");
        }

        private void MusicClick(object sender, RoutedEventArgs e)
        {
            ShowMusicDirectory?.Invoke(this, EventArgs.Empty);
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            if (_mainWindow != null)
            {
                _mainWindow.MainContentFrame.Navigate(new Settings());
            }
            else
            {
                MessageBox.Show("MainWindow is not initialized");
            }
        }

        private void CloseSidebar(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            if (_mainWindow != null)
            {
                _mainWindow.MainContentFrame.Navigate(new StartPage());
            }
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
    }
}
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfTuneForgePlayer
{
    public partial class Sidebar : UserControl
    {
        public event Action<string> MusicSelected;
        public event EventHandler ShowMusicDirectory;
        private MainWindow _mainWindow;

        public Sidebar()
        {
            InitializeComponent();

        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Favorite clicked");
        }

        private void LanguageClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Language clicked");
        }

        private void MusicClick(object sender, RoutedEventArgs e)
        {
            ShowMusicDirectory?.Invoke(this, EventArgs.Empty);

        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings clicked");
        }
        private void CloseSidebar(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainContentFrame.Navigate(new StartPage());
        }
    }
}
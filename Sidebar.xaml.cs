using System;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;

namespace WpfTuneForgePlayer
{
    public partial class Sidebar : UserControl
    {
        public event Action<string> MusicSelected;
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
            var openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                MusicSelected?.Invoke(openFileDialog.FileName);
            }
            
        }
        
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings clicked");
        }
        private void CloseSidebar(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
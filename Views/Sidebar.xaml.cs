﻿using Microsoft.Win32;
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
        public event EventHandler NavigateToSettings;
        public event EventHandler FavoritePage;
        private MainWindow _mainWindow;

        public Sidebar()
        {
            InitializeComponent();
        }

        private void FavoriteClick(object sender, RoutedEventArgs e)
        {
            FavoritePage?.Invoke(this, EventArgs.Empty); 
        }

        private void MusicClick(object sender, RoutedEventArgs e)
        {
            ShowMusicDirectory?.Invoke(this, EventArgs.Empty);
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            NavigateToSettings?.Invoke(this, EventArgs.Empty);
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

    }
}
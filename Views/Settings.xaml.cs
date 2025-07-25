﻿using System;
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
using WpfTuneForgePlayer.AudioModel;

namespace WpfTuneForgePlayer.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public event EventHandler backToStartPage;
        public Settings(DeviceOutputModel deviceOutputModel)
        {
            InitializeComponent();
            this.DataContext = deviceOutputModel;
        }

        private void BackToStartPage(object sender, RoutedEventArgs e)
        {
            backToStartPage?.Invoke(this, EventArgs.Empty);
        }
        private void OpenInfoDialog(object sender, RoutedEventArgs e)
        {
            var authorWindow = new AuthorInfo();
            authorWindow.ShowDialog();
        }

        
    }
}

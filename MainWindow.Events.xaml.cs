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

namespace WpfTuneForgePlayer
{
    public partial class MainWindow : Window
    {
        private void ToggleSidebar(object sender, RoutedEventArgs e)
        {
            if (Sidebar.Visibility == Visibility.Visible)
            {
                SideBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                Sidebar.Visibility = Visibility.Visible;
            }
        }
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
    }
}
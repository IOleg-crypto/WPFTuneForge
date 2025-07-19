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
using WpfTuneForgePlayer.Helpers;

namespace WpfTuneForgePlayer.Views
{
    /// <summary>
    /// Interaction logic for FavoriteSongs.xaml
    /// </summary>
    public partial class FavoriteSongs : Page
    {
        public FavoriteSongs()
        {
            InitializeComponent();

            var songs = new List<Song>
            {
                new Song { Title = "Song A", Artist = "Artist A", Duration = "3:45" },
                new Song { Title = "Song B", Artist = "Artist B", Duration = "4:12" },
                new Song { Title = "Song C", Artist = "Artist C", Duration = "2:58" }
            };

            FavoriteSongsGrid.ItemsSource = songs;

        }

    }
}

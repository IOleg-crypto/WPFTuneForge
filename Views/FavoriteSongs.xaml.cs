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
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.Views
{
    /// <summary>
    /// Interaction logic for FavoriteSongs.xaml
    /// </summary>
    public partial class FavoriteSongs : Page
    {
        private StartPage _startPage;
        private MusicViewModel _viewModel;
        public FavoriteSongs(MusicViewModel vm)
        {
            InitializeComponent();
            _startPage = new StartPage();
            _viewModel = vm;
            DataContext = vm;
            //Test data
            var songs = new List<Song>
            {
                new Song { Title = "Song A", Artist = "Artist A", Duration = "3:45" },
                new Song { Title = "Song B", Artist = "Artist B", Duration = "4:12" },
                new Song { Title = "Song C", Artist = "Artist C", Duration = "2:58" }
            };

            FavoriteSongsGrid.ItemsSource = songs;

        }

        private void FavoriteSongsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                _viewModel.MainWindow = mainWindow;
                _startPage.DataContext = _viewModel;
                mainWindow.MainContentFrame.Navigate(_startPage);
            }
        }
    }
}

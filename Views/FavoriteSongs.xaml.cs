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
        private List<Song> songs;

        public List<Song> Songs { get => songs; set => songs = value; }

        public FavoriteSongs(MusicViewModel vm)
        {
            InitializeComponent();
            _startPage = new StartPage();
            songs = new List<Song>();
            _viewModel = vm;
            DataContext = vm;
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

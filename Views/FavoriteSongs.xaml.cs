using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private ObservableCollection<Song> songs;

        public ObservableCollection<Song> Songs
        {
            get => songs;
            set
            {
                songs = value;
            }
        }

        public FavoriteSongs(MusicViewModel vm , StartPage startPage)
        {
            InitializeComponent();
            FavoriteSongsGrid.Loaded += (s, e) =>
            {
                FavoriteSongsGrid.UpdateLayout();
            };
            _startPage = startPage;
            _viewModel = vm;
            // Needed for binding (instead using DAMNNN MAINWINDOW)
            Songs = _viewModel.SongGrid.Count > 0
            ? _viewModel.SongGrid
            : ReadFile("FavoriteSong.bin");
            vm.SongGrid = songs;
            FavoriteSongsGrid.ItemsSource = vm.SongGrid;
            DataContext = vm;

        }
        // Read user favorite songs from file
        public ObservableCollection<Song> ReadFile(string fileName)
        {
            var result = new ObservableCollection<Song>(); 

            if (!File.Exists(fileName))
                return result;

            var uniqueSongs = new HashSet<Song>();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string artist = reader.ReadString();
                    string title = reader.ReadString();
                    string duration = reader.ReadString();

                    var song = new Song(title, artist, duration);

                    if (uniqueSongs.Add(song))
                    {
                        result.Add(song);
                    }
                    else
                    {
                        SimpleLogger.Log($"Duplicate skipped: {title} - {artist}");
                    }
                }
            }

            return result;
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

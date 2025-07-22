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
                FavoriteSongsGrid.ItemsSource = songs;
            }
        }

        public FavoriteSongs(MusicViewModel vm)
        {
            InitializeComponent();
            _startPage = new StartPage();
            _viewModel = vm;   
            // Needed for binding (instead using DAMNNN MAINWINDOW)
            Songs = _viewModel.SongGrid.Count > 0 ? _viewModel.SongGrid : ReadFile("FavoriteSong.bin");
            DataContext = vm;
            FavoriteSongsGrid.ItemsSource = songs;

        }
        // Read user favorite songs from file
        public ObservableCollection<Song> ReadFile(string FileName)
        {
            Songs = new ObservableCollection<Song>();

            if (!File.Exists(FileName))
            {
                return Songs;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string artist = reader.ReadString();
                    string title = reader.ReadString();
                    string duration = reader.ReadString();

                    Songs.Add(new Song(title, artist, duration));
                }
            }
            return Songs;
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace WpfTuneForgePlayer.Views
{
    /// <summary>
    /// Interaction logic for FavoriteSongs.xaml
    /// </summary>
    public partial class FavoriteSongs : Page
    {
        private readonly StartPage _startPage;
        private readonly MusicViewModel _viewModel;
        private string _pathFileRead;

        public ObservableCollection<Song> Songs { get; private set; }

        public FavoriteSongs(MusicViewModel vm, StartPage startPage)
        {
            InitializeComponent();
            _startPage = startPage;
            _viewModel = vm;

            SetupGrid();
            InitPathReadFile();
            InitializeSongs();
            BindDataContext();
        }

        private void InitPathReadFile()
        {
            _pathFileRead = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FavoriteSong.bin"); 
        }

        private void SetupGrid()
        {
            FavoriteSongsGrid.Loaded += (s, e) =>
            {
                FavoriteSongsGrid.UpdateLayout();
            };
        }

        private void InitializeSongs()
        {
            Songs = _viewModel.SongGrid.Count > 0
                ? _viewModel.SongGrid
                : LoadSongsFromFile(_pathFileRead);

            _viewModel.SongGrid = Songs;
            FavoriteSongsGrid.ItemsSource = _viewModel.SongGrid;
        }


        private void BindDataContext()
        {
            DataContext = _viewModel;
        }

        private ObservableCollection<Song> LoadSongsFromFile(string fileName)
        {
            var result = new ObservableCollection<Song>();

            if (!File.Exists(fileName))
                return result;

            var uniqueSongs = new HashSet<Song>();

            using (var reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
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

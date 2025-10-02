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
            _pathFileRead = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FavoriteSongs.txt");
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

            var uniqueSongs = new HashSet<string>();

            foreach (var line in File.ReadAllLines(fileName))
            {
                var parts = line.Split('|');
                if (parts.Length != 3) continue;

                string artist = parts[0];
                string title = parts[1];
                string duration = parts[2];

                string key = $"{artist}|{title}|{duration}";

                if (uniqueSongs.Add(key))
                {
                    result.Add(new Song(title, artist, duration));
                }
                else
                {
                    SimpleLogger.Log($"Duplicate skipped: {title} - {artist}");
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

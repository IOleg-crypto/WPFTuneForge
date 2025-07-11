using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Model;

namespace WpfTuneForgePlayer.ViewModel
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        // Private fields
        private string _artist = "Unknown";
        private string _title = "Unknown";
        private ImageSource _albumArt;
        private double _trackPosition;
        private string _currentTime = "00:00";
        private string _endTime = "00:00";
        private ImageSource _favoriteSong;

        // Constructor
        public MusicViewModel()
        {
            AlbumArt = LoadImageOrDefault("assets/menu/musicLogo.jpg");
            FavoriteSong = LoadImageOrDefault("assets/sidebar/favorite_a.png");
            InitCommands();
        }

        // Load default image with file existence check
        public ImageSource LoadImageOrDefault(string relativePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (!File.Exists(fullPath))
                return null;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(fullPath, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        // Load album art from TagLib file
        private ImageSource LoadAlbumArt(TagLib.File tagFile)
        {
            if (tagFile.Tag.Pictures.Length == 0) return null;

            var bin = tagFile.Tag.Pictures[0].Data.Data;
            using var ms = new MemoryStream(bin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        // Init commands for UI
        private void InitCommands()
        {
            PlayCommand = new RelayCommand(() => MainWindow?.OnClickMusic(this, null));
            SelectFavoriteSong = new RelayCommand(() => MainWindow?.SelectFavoriteSongToPlayList(this, null));
            _ToggleSound = new RelayCommand(() => MainWindow?.ToggleSound(this, null));
            RepeatCommand = new RelayCommand(() => MainWindow?.RepeatSong(this, null));
            _startMusic = new RelayCommand(() => MainWindow?.StartMusic(this, null));
            _endMusic = new RelayCommand(() => MainWindow?.EndMusic(this, null));

            PlaySelectedSongCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<SongModel>(song =>
            {
                if (song != null)
                {
                    MainWindow.CurrentMusicPath = song.FilePath;
                    MainWindow?.TakeArtistSongName(song.FilePath);
                    MainWindow?.UpdateAlbumArt(song.FilePath);
                }
            });
        }

        public void LoadSongs(string folder)
        {
            Songs.Clear();
            var files = Directory.GetFiles(folder, "*.mp3");
            foreach (var path in files)
            {
                var file = TagLib.File.Create(path);
                Songs.Add(new SongModel
                {
                    Title = file.Tag.Title ?? Path.GetFileNameWithoutExtension(path),
                    Artist = file.Tag.FirstPerformer ?? "Unknown",
                    Duration = file.Properties.Duration.ToString(@"mm\:ss"),
                    FilePath = path,
                    AlbumArt = file.Tag.Pictures.Length == 0
                        ? LoadImageOrDefault("assets/menu/musicLogo.jpg")
                        : LoadAlbumArt(file)
                });
            }
        }

        // Public properties
        public ObservableCollection<SongModel> Songs { get; set; } = new();
        public MainWindow MainWindow { get; set; }

        public string Artist
        {
            get => _artist;
            set { _artist = value; OnPropertyChanged(nameof(Artist)); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public ImageSource AlbumArt
        {
            get => _albumArt;
            set { _albumArt = value; OnPropertyChanged(nameof(AlbumArt)); }
        }

        public ImageSource FavoriteSong
        {
            get => _favoriteSong;
            set { _favoriteSong = value; OnPropertyChanged(nameof(FavoriteSong)); }
        }

        public double TrackPosition
        {
            get => _trackPosition;
            set { _trackPosition = value; OnPropertyChanged(nameof(TrackPosition)); }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set { _currentTime = value; OnPropertyChanged(nameof(CurrentTime)); }
        }

        public string EndTime
        {
            get => _endTime;
            set { _endTime = value; OnPropertyChanged(nameof(EndTime)); }
        }

        // Commands
        public ICommand PlayCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand SelectFavoriteSong { get; set; }
        public ICommand _ToggleSound { get; set; }
        public ICommand _startMusic { get; set; }
        public ICommand _endMusic { get; set; }
        public ICommand PlaySelectedSongCommand { get; set; }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

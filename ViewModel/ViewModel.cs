using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Model;
using WinForm = System.Windows.Forms;



namespace WpfTuneForgePlayer.ViewModel
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        // ===== Private fields =====
        private string _artist = "Unknown";
        private string _title = "Unknown";
        private ImageSource _albumArt;
        private double _trackPosition;
        private string _currentTime = "00:00";
        private string _endTime = "00:00";
        private ImageSource _favoriteSong;
        private ImageSource _soundStatus; // Icon that shows whether sound is muted or not
        private AudioService audioService;
        private AudioMetaService audioMetaService;
        private DeviceOutputModel __deviceOutputModel;
        public DeviceOutputModel DeviceOutputModel => __deviceOutputModel;



        private bool _isMonoEnabled;
        // Force mono output
        public bool IsMonoEnabled
        {
            get => _isMonoEnabled;
            set { _isMonoEnabled = value; OnPropertyChanged(nameof(IsMonoEnabled)); }
        }

        // Supported audio file extensions
        private List<string> SupportedExtensionsSong = new List<string>()
        {
            ".wav",  // WaveFileReader
            ".aiff", // AiffFileReader
            ".mp3",  // Mp3FileReader
            ".wma",  // MediaFoundationReader
            ".aac",  // MediaFoundationReader
            ".mp4",  // MediaFoundationReader (audio stream only)
            ".ogg",  // Requires NVorbis
            ".flac", // Requires NAudio.Flac
        };

        public string TakeCurrentDirectory { get; set; }
        

        // ===== Constructor =====
        public MusicViewModel()
        {
            AlbumArt = LoadImageOrDefault("assets/menu/musicLogo.jpg");
            FavoriteSong = LoadImageOrDefault("assets/sidebar/favorite_a.png");
            SoundStatus = LoadImageOrDefault("assets/menu/volume-high_new.png");
            InitAudioService();
            InitCommands();
        }

        private void InitAudioService()
        {
            audioService = new AudioService(this);
            audioMetaService = new AudioMetaService(this);
            __deviceOutputModel = new DeviceOutputModel();
            __deviceOutputModel.StartDeviceMonitoring();
        }

        // Load an image from file or return null if not found
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
            image.Freeze(); // Makes it cross-thread accessible
            return image;
        }

        // Extract album art from a TagLib file
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

        // ===== Initialize UI Commands =====
        private void InitCommands()
        {
            PlayCommand = new RelayCommand(() => audioService.OnClickMusic(this, null));
            SelectFavoriteSong = new RelayCommand(() => audioService?.SelectFavoriteSongToPlayList(this, null));
            _ToggleSound = new RelayCommand(() => audioService?.ToggleSound(this, null));
            RepeatCommand = new RelayCommand(() => audioService?.RepeatSong(this, null));
            _startMusic = new RelayCommand(() => audioService?.StartMusic(this, null));
            _endMusic = new RelayCommand(() => audioService?.EndMusic(this, null));
            toggleAudio = new RelayCommand(() => audioService?.ToggleSound(this, null));
            changeMusicTime = new RelayCommand(() => audioService?.SliderChanged());
            reloadMusicPage = new RelayCommand(() => LoadSongs(TakeCurrentDirectory));
            TakeTimer = new RelayCommand(() => audioService._timerHelper?.TimerTime_Tick(null, null));
            SelectChaoticallySong = new RelayCommand(() => audioService?.ChaoticPlaySong(this, null));

            // When a song is selected from the list, set it as current and update UI
            PlaySelectedSongCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<SongModel>(song =>
            {
                if (song != null)
                {
                    audioService.CurrentMusicPath = song.FilePath;
                    audioMetaService.TakeArtistSongName(song.FilePath);
                    audioMetaService?.UpdateAlbumArt(song.FilePath);
                }
            });
        }

        // Load all songs with supported extensions from given folder
        public void LoadSongs(string folder)
        {
            if (String.IsNullOrEmpty(folder)) return;

            Songs.Clear();

            // Find all matching audio files
            var files = SupportedExtensionsSong
                .SelectMany(ext => Directory.GetFiles(folder, "*" + ext, SearchOption.AllDirectories))
                .ToList();

            // Populate the Songs collection
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

        // ===== Public Properties (bindable in XAML) =====
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

        public ImageSource SoundStatus
        {
            get => _soundStatus;
            set { _soundStatus = value; OnPropertyChanged(nameof(SoundStatus)); }
        }

        // Whether the slider is currently allowed to be moved by user
        public bool GetStatusOnSlider
        {
            get => audioService.isSliderEnabled;
            set { audioService.isSliderEnabled = value; OnPropertyChanged(nameof(GetStatusOnSlider)); }
        }

        // ===== Commands (bindable in XAML via MVVM) =====
        public ICommand PlayCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand SelectFavoriteSong { get; set; }
        public ICommand _ToggleSound { get; set; }
        public ICommand _startMusic { get; set; }
        public ICommand _endMusic { get; set; }
        public ICommand PlaySelectedSongCommand { get; set; }
        public ICommand toggleAudio { get; set; }        // Toggle mute/unmute
        public ICommand changeMusicTime { get; set; }    // Handle slider time change
        public ICommand reloadMusicPage { get; set; }    // Reload songs in case of changes

        public ICommand TakeTimer { get; set; }

        public ICommand SelectChaoticallySong { get; set; }

        // ===== INotifyPropertyChanged implementation =====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        

        
    }
}

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
using WpfTuneForgePlayer.Helpers;



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
        private ImageSource playPauseButton;
        private AudioService audioService;
        private AudioMetaService audioMetaService;
        private DeviceOutputModel __deviceOutputModel;
        

        

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
            AlbumArt = ImageLoader.LoadImageOrDefault("assets/menu/musicLogo.jpg");
            FavoriteSong = ImageLoader.LoadImageOrDefault("assets/sidebar/favorite_a.png");
            SoundStatus = ImageLoader.LoadImageOrDefault("assets/menu/volume-high_new.png");
            playPauseButton = ImageLoader.LoadImageOrDefault("assets/menu/play.png");
            InitAudioService();
            
        }

        private void InitAudioService()
        {
            audioService = new AudioService(this);
            audioMetaService = new AudioMetaService(this);
            __deviceOutputModel = new DeviceOutputModel(audioService , this , audioMetaService);
            __deviceOutputModel.StartDeviceMonitoring();
            InitICommand();
            
        }

        private void InitICommand()
        {
            Commands = new BindingCommands();
            Commands.InitCommands(this, audioService, audioMetaService);
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
                        ? ImageLoader.LoadImageOrDefault("assets/menu/musicLogo.jpg")
                        : ImageLoader.LoadAlbumArt(file)
                });
            }
        }


        // ===== Public Properties (bindable in XAML) =====
        public ObservableCollection<SongModel> Songs { get; set; } = new();
        public ObservableCollection<Song> SongGrid { get
                ; set; } = new();
        public BindingCommands Commands { get; private set; }
        public MainWindow MainWindow { get; set; }
        public DeviceOutputModel DeviceOutputModel => __deviceOutputModel;

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex= value;
            }
        }

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
            get => audioService.IsSliderEnabled;
            set { audioService.IsSliderEnabled = value; OnPropertyChanged(nameof(GetStatusOnSlider)); }
        }

        public ImageSource PlayPauseButton
        {
            get => playPauseButton;
            set { playPauseButton = value; OnPropertyChanged(nameof(PlayPauseButton)); }
        }

        

        // ===== INotifyPropertyChanged implementation =====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}

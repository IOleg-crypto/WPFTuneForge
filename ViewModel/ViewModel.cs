using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Model;
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
        private ImageSource _soundStatus; // Icon showing if sound is muted or not
        private ImageSource playPauseButton;
        private AudioService audioService;
        private AudioMetaService audioMetaService;
        private DeviceOutputModel __deviceOutputModel;

        // Supported audio file extensions for loading songs
        private readonly List<string> SupportedExtensionsSong = new()
        {
            ".wav",   // WaveFileReader
            ".aiff",  // AiffFileReader
            ".mp3",   // Mp3FileReader
            ".wma",   // MediaFoundationReader
            ".aac",   // MediaFoundationReader
            ".mp4",   // MediaFoundationReader (audio only)
            ".ogg",   // Requires NVorbis
            ".flac",  // Requires NAudio.Flac
        };

        // Directory path currently used to load songs
        public string TakeCurrentDirectory { get; set; }

        // ===== Constructor =====
        public MusicViewModel()
        {
            // Load default images for UI elements
            AlbumArt = ImageLoader.LoadImageOrDefault("assets/menu/musicLogo.jpg");
            FavoriteSong = ImageLoader.LoadImageOrDefault("assets/sidebar/favorite_a.png");
            SoundStatus = ImageLoader.LoadImageOrDefault("assets/menu/volume-high_new.png");
            playPauseButton = ImageLoader.LoadImageOrDefault("assets/menu/play.png");

            // Initialize audio related services and commands
            InitAudioService();
        }

        // Initialize AudioService, AudioMetaService, DeviceOutputModel and commands
        private void InitAudioService()
        {
            audioService = new AudioService(this);
            audioMetaService = new AudioMetaService(this);

            __deviceOutputModel = new DeviceOutputModel(audioService, this, audioMetaService);
            __deviceOutputModel.StartDeviceMonitoring();

            audioService.DeviceOutputModel = DeviceOutputModel;

            InitICommand();
        }

        // Initialize UI commands and bind them to appropriate handlers
        private void InitICommand()
        {
            Commands = new BindingCommands();
            Commands.InitCommands(this, audioService, audioMetaService);
        }

        /// <summary>
        /// Load all songs with supported extensions from the specified folder.
        /// </summary>
        /// <param name="folder">Path to directory to scan for audio files</param>
        public void LoadSongs(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return;

            Songs.Clear();

            // Search files recursively with supported extensions
            var files = SupportedExtensionsSong
                .SelectMany(ext => Directory.GetFiles(folder, "*" + ext, SearchOption.AllDirectories))
                .ToList();

            // Create SongModel for each file and add to Songs collection
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

        // ===== Public properties (bindable in XAML) =====
        public ObservableCollection<SongModel> Songs { get; set; } = new();
        public ObservableCollection<Song> SongGrid { get; set; } = new();
        public BindingCommands Commands { get; private set; }
        public MainWindow MainWindow { get; set; }
        public DeviceOutputModel DeviceOutputModel { get; set; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
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

        /// <summary>
        /// Indicates whether the track position slider is enabled for user interaction.
        /// </summary>
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

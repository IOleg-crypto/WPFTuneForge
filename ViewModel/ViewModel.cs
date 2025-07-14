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
        private int _selectedOutputDeviceIndex;
        private DispatcherTimer _deviceCheckTimer;

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
        public ObservableCollection<string> OutputDevices { get; set; } = new();

        // ===== Constructor =====
        public MusicViewModel()
        {
            AlbumArt = LoadImageOrDefault("assets/menu/musicLogo.jpg");
            FavoriteSong = LoadImageOrDefault("assets/sidebar/favorite_a.png");
            SoundStatus = LoadImageOrDefault("assets/menu/volume-high_new.png");
            InitCommands();
            StartDeviceMonitoring();
            LoadOutputDevices();
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
            PlayCommand = new RelayCommand(() => MainWindow?.OnClickMusic(this, null));
            SelectFavoriteSong = new RelayCommand(() => MainWindow?.SelectFavoriteSongToPlayList(this, null));
            _ToggleSound = new RelayCommand(() => MainWindow?.ToggleSound(this, null));
            RepeatCommand = new RelayCommand(() => MainWindow?.RepeatSong(this, null));
            _startMusic = new RelayCommand(() => MainWindow?.StartMusic(this, null));
            _endMusic = new RelayCommand(() => MainWindow?.EndMusic(this, null));
            toggleAudio = new RelayCommand(() => MainWindow?.ToggleSound(this, null));
            changeMusicTime = new RelayCommand(() => MainWindow?.SliderChanged());
            reloadMusicPage = new RelayCommand(() => LoadSongs(TakeCurrentDirectory));

            // When a song is selected from the list, set it as current and update UI
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
            get => MainWindow.isSliderEnabled;
            set { MainWindow.isSliderEnabled = value; OnPropertyChanged(nameof(GetStatusOnSlider)); }
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

        // ===== INotifyPropertyChanged implementation =====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _selectedOutputDevice;
        public string SelectedOutputDevice
        {
            get => _selectedOutputDevice;
            set
            {
                if (_selectedOutputDevice != value)
                {
                    _selectedOutputDevice = value;
                    OnPropertyChanged(nameof(SelectedOutputDevice));
                }
            }
        }

        public void StartDeviceMonitoring()
        {
            // Better DispatcherTimer than Timer(from WinForm - it shit , which kill performance at all)
            _deviceCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            _deviceCheckTimer.Tick += (s, e) => LoadOutputDevices();
            _deviceCheckTimer.Start();
        }

        private void LoadOutputDevices()
        {
            try
            {
                var enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                var deviceNames = devices.Select(d => d.FriendlyName).ToList();


                var currentDevices = OutputDevices.ToList();

                bool devicesChanged = deviceNames.Count != currentDevices.Count ||
                                      !deviceNames.SequenceEqual(currentDevices);

                var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string defaultDeviceName = defaultDevice?.FriendlyName;
                // Needed to check if default device changed , to avoid infinite check
                if (devicesChanged || defaultDeviceName != SelectedOutputDevice)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (devicesChanged)
                        {
                            OutputDevices.Clear();
                            foreach (var name in deviceNames)
                                OutputDevices.Add(name);
                        }

                        SelectedOutputDevice = defaultDeviceName;
                    });
                }
            }
            catch (Exception ex)
            {
                WinForm.MessageBox.Show(ex.Message, "Error with devices", WinForm.MessageBoxButtons.OK, WinForm.MessageBoxIcon.Error);
            }
        }
    }
}

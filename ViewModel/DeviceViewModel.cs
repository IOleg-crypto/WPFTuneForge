using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Helpers;

namespace WpfTuneForgePlayer.ViewModel
{
    public class DeviceOutputModel : INotifyPropertyChanged
    {
        private DispatcherTimer _deviceCheckTimer;

        private bool AutomaticPlayback = false;

        private AudioService audioService;
        private MusicViewModel viewModel;
        private AudioMetaService audioMetaService;

        public ObservableCollection<AudioDeviceInfo> OutputDevices { get; set; } = new();

        public DeviceOutputModel(AudioService audioService , MusicViewModel viewModel , AudioMetaService audioMetaService)
        {
            this.audioService = audioService;
            this.viewModel = viewModel;
            this.audioMetaService = audioMetaService;
            StartDeviceMonitoring();
        }


        private AudioDeviceInfo _selectedOutputDevice;
        public AudioDeviceInfo SelectedOutputDevice
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

        public AudioMetaService AudioMetaService => audioMetaService;
        public AudioService AudioService => audioService;

        public bool IsAutomaticPlayback
        {
            get => AutomaticPlayback;
            set { AutomaticPlayback = value; 
                if (AutomaticPlayback)
                {
                    AutomaticPlayback = value;
                    OnPropertyChanged(nameof(IsAutomaticPlayback));
                    IsAutomaticPlaybackChanged(); 
                }
                OnPropertyChanged(nameof(IsAutomaticPlayback)); }
        }

        private void IsAutomaticPlaybackChanged()
        {

            if (IsAutomaticPlayback &&
           TimeSpan.TryParse(audioService.MusicViewModel.CurrentTime, out var current) &&
           TimeSpan.TryParse(audioService.MusicViewModel.EndTime, out var end) &&
           current >= end)
            {
                if (viewModel.Songs.Count == 0)
                {
                    MessageBox.Show("No music available.", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (AudioService.AudioFile != null)
                {
                    AudioService.AudioFile.CurrentTime = AudioService.AudioFile.TotalTime - TimeSpan.FromMilliseconds(500);
                }
                AudioService.IsSliderEnabled = true;
                AudioService.IsSelectedSongFavorite = true;

                int updatedIndex = viewModel.SelectedIndex + 1;

                if (updatedIndex < viewModel.Songs.Count)
                {
                    viewModel.SelectedIndex = updatedIndex;
                    audioService.CurrentMusicPath = viewModel.Songs[updatedIndex].FilePath;
                    SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
                }
                else
                {
                    SimpleLogger.Log("Reached the end of the playlist.");
                    updatedIndex = 0;
                    viewModel.SelectedIndex = updatedIndex;
                    audioService.CurrentMusicPath = viewModel.Songs[updatedIndex].FilePath;
                    SimpleLogger.Log($"Playing music: {audioService.CurrentMusicPath}");
                }

                audioService.StopAndDisposeCurrentMusic();

                try
                {
                    audioMetaService.TakeArtistSongName(audioService.CurrentMusicPath);
                    audioMetaService?.UpdateAlbumArt(audioService.CurrentMusicPath);

                    audioService.AudioFile = new AudioFileReader(audioService.CurrentMusicPath);

                    if (audioService.OutputDevice == null)
                    {
                        audioService.OutputDevice = new WaveOutEvent();
                        audioService.OutputDevice.PlaybackStopped += audioService.OnPlaybackStopped;
                    }


                    audioService.OutputDevice.Init(audioService.AudioFile);
                    audioService.OutputDevice.Play();

                    audioService.NewMusicPath = audioService.CurrentMusicPath;
                    audioService.IsMusicPlaying = true;
                    audioService.TimerHelper.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error:: {ex.Message}", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        



        public void StartDeviceMonitoring()
        {
            _deviceCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4)
            };
            _deviceCheckTimer.Tick += (s, e) => LoadOutputDevices();
            _deviceCheckTimer.Start();
            LoadOutputDevices();
        }

        public void StopDeviceMonitoring()
        {
            _deviceCheckTimer?.Stop();
            _deviceCheckTimer = null;
        }

        private void LoadOutputDevices()
        {
            try
            {
                var enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

                var outputDevices = devices.Select(d => new AudioDeviceInfo
                {
                    Name = d.FriendlyName,
                    Channels = d.AudioClient.MixFormat.Channels,
                    SampleRate = d.AudioClient.MixFormat.SampleRate
                }).ToList();

                var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string defaultDeviceName = defaultDevice?.FriendlyName;

                bool devicesChanged = outputDevices.Count != OutputDevices.Count;

                // Null check
                var firstDeviceName = OutputDevices.FirstOrDefault()?.Name;
                if (devicesChanged || firstDeviceName != defaultDeviceName)
                {
                    if (App.Current.Dispatcher.CheckAccess())
                        UpdateDevices(outputDevices, defaultDeviceName);
                    else
                        App.Current.Dispatcher.Invoke(() => UpdateDevices(outputDevices, defaultDeviceName));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Device check error: {ex.Message}");
            }
        }

        private void UpdateDevices(List<AudioDeviceInfo> newDevices, string defaultDeviceName)
        {
            OutputDevices.Clear();
            foreach (var device in newDevices)
            {
                OutputDevices.Add(device);
            }

            var defaultDevice = newDevices.FirstOrDefault(d => d.Name == defaultDeviceName);

            if (defaultDevice != null)
                SelectedOutputDevice = defaultDevice;
            else if (newDevices.Count > 0)
                SelectedOutputDevice = newDevices[0];
            else
                SelectedOutputDevice = null;
        }

        // ==== INotifyPropertyChanged ====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

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
        private DispatcherTimer _playbackTimer;

        private bool _isSwitchingSong = false;
        private bool _automaticPlayback = false;

        private AudioService audioService;
        private MusicViewModel viewModel;
        private AudioMetaService audioMetaService;

        public ObservableCollection<AudioDeviceInfo> OutputDevices { get; } = new();

        public DeviceOutputModel(AudioService audioService, MusicViewModel viewModel, AudioMetaService audioMetaService)
        {
            this.audioService = audioService;
            this.viewModel = viewModel;
            this.audioMetaService = audioMetaService;

            StartDeviceMonitoring();
            StartPlaybackTimer();
        }

        // --- Properties ---

        public MusicViewModel ViewModel => viewModel;

        public bool SwitchingSong => _isSwitchingSong;

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
        private AudioDeviceInfo _selectedOutputDevice;

        public AudioMetaService AudioMetaService => audioMetaService;

        public AudioService AudioService => audioService;

        public bool IsAutomaticPlayback
        {
            get => _automaticPlayback;
            set
            {
                if (_automaticPlayback != value)
                {
                    _automaticPlayback = value;
                    SimpleLogger.Log($"Setting playback : {_automaticPlayback}");
                    OnPropertyChanged(nameof(IsAutomaticPlayback));
                }
            }
        }

        // --- Device monitoring ---

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
                OutputDevices.Add(device);

            var defaultDevice = newDevices.FirstOrDefault(d => d.Name == defaultDeviceName);

            SelectedOutputDevice = defaultDevice ?? newDevices.FirstOrDefault();
        }

        // --- Playback timer and controls ---

        private void StartPlaybackTimer()
        {
            _playbackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(300)
            };
            _playbackTimer.Tick += (s, e) => CheckPlaybackPosition();
            _playbackTimer.Start();
        }

        private void CheckPlaybackPosition()
        {
            if (IsAutomaticPlayback == false)
                return;

            if (TimeSpan.TryParse(viewModel.CurrentTime, out TimeSpan current) &&
                TimeSpan.TryParse(viewModel.EndTime, out TimeSpan end))
            {
                SimpleLogger.Log($"Current time: {current}, End time: {end}");

                if (current >= end - TimeSpan.FromMilliseconds(300))
                {
                    if (!_isSwitchingSong)
                    {
                        _isSwitchingSong = true;
                        AudioService.MusicNavigationService.EndMusic(this, null);
                    }
                }
                else
                {
                    _isSwitchingSong = false;
                }
            }
        }
        

        // --- INotifyPropertyChanged implementation ---

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

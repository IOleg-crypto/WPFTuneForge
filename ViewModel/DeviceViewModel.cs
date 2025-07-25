using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using TagLib.Mpeg;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.Helpers;

namespace WpfTuneForgePlayer.ViewModel
{
    public class DeviceOutputModel : INotifyPropertyChanged
    {
        private DispatcherTimer _deviceCheckTimer;
        private bool _automaticPlayback;

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
        }

        // --- Properties ---

        public MusicViewModel ViewModel => viewModel;

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

        public AudioMetaService AudioMetaService
        {
            get => audioMetaService;
            set => audioMetaService = value;
        }

        public AudioService AudioService
        {
            get => audioService;
            //set { SimpleLogger.Log($"CheckPlaybackPosition: AudioService is {(AudioService == null ? "null" : "not null")}"); audioService = value; SimpleLogger.Log($"DeviceOutputModel ctor: AudioService is {(audioService == null ? "null" : "not null")}"); }
        }

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
        // --- INotifyPropertyChanged implementation ---

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

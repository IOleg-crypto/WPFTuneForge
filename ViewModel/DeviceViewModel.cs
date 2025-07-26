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
        // Timer for periodically checking audio output devices
        private DispatcherTimer _deviceCheckTimer;

        // Flag for automatic playback mode
        private bool _automaticPlayback;

        // References to services and view model
        private AudioService audioService;
        private MusicViewModel viewModel;
        private AudioMetaService audioMetaService;

        // Collection of available audio output devices for UI binding
        public ObservableCollection<AudioDeviceInfo> OutputDevices { get; } = new();

        // Currently selected audio output device
        private AudioDeviceInfo _selectedOutputDevice;

        // Constructor - initializes with dependencies and starts device monitoring
        public DeviceOutputModel(AudioService audioService, MusicViewModel viewModel, AudioMetaService audioMetaService)
        {
            this.audioService = audioService;
            this.viewModel = viewModel;
            this.audioMetaService = audioMetaService;

            StartDeviceMonitoring();
        }

        // Expose ViewModel for binding or internal use
        public MusicViewModel ViewModel => viewModel;

        /// <summary>
        /// Selected audio output device from the list
        /// Notifies UI on change.
        /// </summary>
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

        public AudioMetaService AudioMetaService
        {
            get => audioMetaService;
            set => audioMetaService = value;
        }

        public AudioService AudioService
        {
            get => audioService;
            // Setter is commented out, but logging could be added here if needed
        }

        /// <summary>
        /// Property controlling whether playback should be automatic.
        /// Logs state changes and notifies UI.
        /// </summary>
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

        // --- Device monitoring methods ---

        /// <summary>
        /// Starts periodic monitoring of audio output devices.
        /// </summary>
        public void StartDeviceMonitoring()
        {
            _deviceCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4)
            };
            _deviceCheckTimer.Tick += (s, e) => LoadOutputDevices();
            _deviceCheckTimer.Start();

            // Initial device load
            LoadOutputDevices();
        }

        /// <summary>
        /// Stops device monitoring timer.
        /// </summary>
        public void StopDeviceMonitoring()
        {
            _deviceCheckTimer?.Stop();
            _deviceCheckTimer = null;
        }

        /// <summary>
        /// Loads the list of active audio output devices and updates the UI if needed.
        /// </summary>
        private void LoadOutputDevices()
        {
            try
            {
                var enumerator = new MMDeviceEnumerator();

                // Get active render (output) devices
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

                // Map devices to AudioDeviceInfo model for UI binding
                var outputDevices = devices.Select(d => new AudioDeviceInfo
                {
                    Name = d.FriendlyName,
                    Channels = d.AudioClient.MixFormat.Channels,
                    SampleRate = d.AudioClient.MixFormat.SampleRate
                }).ToList();

                // Get the system default output device name
                var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string defaultDeviceName = defaultDevice?.FriendlyName;

                // Check if the device list or default device changed
                bool devicesChanged = outputDevices.Count != OutputDevices.Count;
                var firstDeviceName = OutputDevices.FirstOrDefault()?.Name;

                if (devicesChanged || firstDeviceName != defaultDeviceName)
                {
                    // Use UI thread to update the device list and selected device
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

        /// <summary>
        /// Updates the OutputDevices collection and selects the default device.
        /// </summary>
        /// <param name="newDevices">List of currently available devices</param>
        /// <param name="defaultDeviceName">Name of the system default device</param>
        private void UpdateDevices(List<AudioDeviceInfo> newDevices, string defaultDeviceName)
        {
            OutputDevices.Clear();

            foreach (var device in newDevices)
                OutputDevices.Add(device);

            // Select the default device if available, otherwise the first device in the list
            var defaultDevice = newDevices.FirstOrDefault(d => d.Name == defaultDeviceName);

            SelectedOutputDevice = defaultDevice ?? newDevices.FirstOrDefault();
        }

        // --- INotifyPropertyChanged implementation ---

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using WpfTuneForgePlayer.AudioModel;

namespace WpfTuneForgePlayer.ViewModel
{
    public class DeviceOutputModel : INotifyPropertyChanged
    {
        private DispatcherTimer _deviceCheckTimer;

        private bool AutomaticPlayback = false;

        private AudioService audioService;

        public ObservableCollection<AudioDeviceInfo> OutputDevices { get; set; } = new();

        public DeviceOutputModel(AudioService audioService )
        {
            this.audioService = audioService;
            StartDeviceMonitoring();
            IsAutomaticPlaybackChanged(this , null);
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

        public bool IsAutomaticPlayback
        {
            get => AutomaticPlayback;
            set { AutomaticPlayback = value; OnPropertyChanged(nameof(IsAutomaticPlayback)); }
        }

        private void IsAutomaticPlaybackChanged(object sender, PropertyChangedEventArgs e)
        {
  
             if (IsAutomaticPlayback &&
                    audioService.musicViewModel.CurrentTime == audioService.musicViewModel.EndTime)
              {
                    audioService._musicNavigationService.EndMusic(this, null);
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

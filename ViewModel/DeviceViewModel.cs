using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using WinForm = System.Windows.Forms;

namespace WpfTuneForgePlayer.ViewModel
{
    public class DeviceOutputModel : INotifyPropertyChanged
    {
        private DispatcherTimer _deviceCheckTimer;

        public ObservableCollection<string> OutputDevices { get; set; } = new();

        public DeviceOutputModel() => StartDeviceMonitoring();

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
                var enumerator = new MMDeviceEnumerator(); // без using

                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                var deviceNames = devices.Select(d => d.FriendlyName).ToList();

                bool devicesChanged = deviceNames.Count != OutputDevices.Count ||
                                      !deviceNames.SequenceEqual(OutputDevices);

                var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string defaultDeviceName = defaultDevice?.FriendlyName;

                if (devicesChanged || defaultDeviceName != SelectedOutputDevice)
                {
                    if (App.Current.Dispatcher.CheckAccess())
                        UpdateDevices(deviceNames, defaultDeviceName);
                    else
                        App.Current.Dispatcher.Invoke(() => UpdateDevices(deviceNames, defaultDeviceName));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Device check error: {ex.Message}");
            }
        }

        private void UpdateDevices(List<string> deviceNames, string defaultDeviceName)
        {
            OutputDevices.Clear();
            foreach (var name in deviceNames)
            {
                OutputDevices.Add(name);
            }


            if (deviceNames.Contains(defaultDeviceName))
                SelectedOutputDevice = defaultDeviceName;
            else if (deviceNames.Count > 0)
                SelectedOutputDevice = deviceNames[0];
            else
                SelectedOutputDevice = null;
        }

        // ==== INotifyPropertyChanged ====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

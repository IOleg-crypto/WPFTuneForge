using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading; 
using WinForm = System.Windows.Forms;
using System.Collections.ObjectModel;

namespace WpfTuneForgePlayer.ViewModel
{
    public class DeviceOutputModel : INotifyPropertyChanged
    {
        private DispatcherTimer _deviceCheckTimer;
        public ObservableCollection<string> OutputDevices { get; set; } = new();
        private string _selectedOutputDevice;
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
            // Not regular situation, but just in case
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
        // ===== INotifyPropertyChanged implementation =====
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

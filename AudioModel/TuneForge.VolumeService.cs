using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.AudioModel
{
    public class VolumeService
    {
        private readonly MMDeviceEnumerator enumerator = new();

        private AudioService audioService;
        private MusicViewModel m_viewModel;

        public VolumeService(AudioService audioService , MusicViewModel viewModel)
        {
            this.audioService = audioService;
            this.m_viewModel = viewModel;
        }

        public void IncreaseVolume()
        {
            var device = GetDefaultDevice();
            device.AudioEndpointVolume.MasterVolumeLevelScalar = Math.Min(device.AudioEndpointVolume.MasterVolumeLevelScalar + 0.1f, 1.0f);
        }

        public void DecreaseVolume()
        {
            var device = GetDefaultDevice();
            device.AudioEndpointVolume.MasterVolumeLevelScalar = Math.Max(device.AudioEndpointVolume.MasterVolumeLevelScalar - 0.1f, 0.0f);
        }

        public void ToggleMute()
        {
            var device = GetDefaultDevice();
            if (audioService._outputDevice == null) return;

            bool currentlyMuted = device.AudioEndpointVolume.Mute;
            SimpleLogger.Log("Current ToggleSound");
            audioService.isSound = currentlyMuted;

            if (audioService.isSound)
            {
                device.AudioEndpointVolume.Mute = false;

                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_new.png");
                SimpleLogger.Log("Changed icon: " + imagePath);
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                }
                m_viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            else
            {
                device.AudioEndpointVolume.Mute = true;

                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_c.png");
                SimpleLogger.Log("Changed icon: " + imagePath);
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                }
                m_viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }

            SimpleLogger.Log("System mute state: " + device.AudioEndpointVolume.Mute);
        }

        private MMDevice GetDefaultDevice()
        {
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }
    }
}

using NAudio.CoreAudioApi;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.Helpers;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.AudioModel
{
    public class VolumeService
    {
        // Enumerator for managing audio devices
        private readonly MMDeviceEnumerator enumerator = new();

        private AudioService audioService;
        private MusicViewModel m_viewModel;

        // Constructor to initialize audio service and view model references
        public VolumeService(AudioService audioService, MusicViewModel viewModel)
        {
            this.audioService = audioService;
            this.m_viewModel = viewModel;
        }

        // Increases the system volume by 10%, clamped to a maximum of 100%
        public void IncreaseVolume()
        {
            var device = GetDefaultDevice();
            device.AudioEndpointVolume.MasterVolumeLevelScalar =
                Math.Min(device.AudioEndpointVolume.MasterVolumeLevelScalar + 0.1f, 1.0f);
        }

        // Decreases the system volume by 10%, clamped to a minimum of 0%
        public void DecreaseVolume()
        {
            var device = GetDefaultDevice();
            device.AudioEndpointVolume.MasterVolumeLevelScalar =
                Math.Max(device.AudioEndpointVolume.MasterVolumeLevelScalar - 0.1f, 0.0f);
        }

        // Toggles system mute state and updates icon in the UI
        public void ToggleSound()
        {
            var device = GetDefaultDevice();
            if (audioService.OutputDevice == null)
                return;

            bool currentlyMuted = device.AudioEndpointVolume.Mute;
            SimpleLogger.Log("Current ToggleSound");

            // Store current mute state in audioService
            audioService.IsSound = currentlyMuted;

            if (audioService.IsSound)
            {
                // Unmute and update icon to volume-high
                device.AudioEndpointVolume.Mute = false;
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_new.png");
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
                // Mute and update icon to muted version
                device.AudioEndpointVolume.Mute = true;
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\menu\\volume-high_c.png");
                SimpleLogger.Log("Changed icon: " + imagePath);

                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Image not found", "TuneForge", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                }

                m_viewModel.SoundStatus = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }

            // Log current mute state
            SimpleLogger.Log("System mute state: " + device.AudioEndpointVolume.Mute);
        }

        // Returns the default output audio device
        private MMDevice GetDefaultDevice()
        {
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }
    }
}

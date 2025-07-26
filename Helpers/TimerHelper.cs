using NAudio.Wave;
using System;
using System.Windows.Threading;
using TagLib.Mpeg;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.Helpers
{
    public class TimerHelper
    {
        private DispatcherTimer timer;
        private AudioService _audioService;
        private MusicViewModel _viewModel;

        // Constructor initializes the timer with specified interval and references to audio service and view model
        public TimerHelper(TimeSpan interval, AudioService audioService, MusicViewModel viewModel)
        {
            _audioService = audioService;
            _viewModel = viewModel;

            timer = new DispatcherTimer
            {
                Interval = interval
            };

            // Attach Tick event handler to update playback progress periodically
            timer.Tick += TimerTime_Tick;
        }

        // Starts the timer
        public void Start()
        {
            timer.Start();
        }

        // Stops the timer
        public void Stop()
        {
            timer.Stop();
        }

        // Event handler called on each timer tick to update UI playback progress
        public void TimerTime_Tick(object sender, EventArgs e)
        {
            // Return early if no audio loaded or music is not playing
            if (_audioService.AudioFile == null || !_audioService.IsMusicPlaying)
                return;

            // Update UI only if playback is ongoing
            if (_audioService.OutputDevice != null && _audioService.OutputDevice.PlaybackState == PlaybackState.Playing)
            {
                // Calculate playback progress as fraction
                double progress = _audioService.AudioFile.CurrentTime.TotalSeconds / _audioService.AudioFile.TotalTime.TotalSeconds;

                // Update track position slider maximum-scaled value
                _viewModel.TrackPosition = progress * _audioService.StartPage.MusicTrackBar.Maximum;

                // Update current playback time display (formatted as mm:ss)
                _viewModel.CurrentTime = _audioService.AudioFile.CurrentTime.ToString(@"mm\:ss");

                // Update total playback time display (formatted as mm:ss)
                _viewModel.EndTime = _audioService.AudioFile.TotalTime.ToString(@"mm\:ss");
            }
        }
    }
}

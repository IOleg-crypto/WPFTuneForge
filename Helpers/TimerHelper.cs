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

        public TimerHelper(TimeSpan interval, AudioService audioService, MusicViewModel viewModel)
        {
            _audioService = audioService;
            _viewModel = viewModel;

            timer = new DispatcherTimer
            {
                Interval = interval
            };
            timer.Tick += TimerTime_Tick;
        }

        public void Start() {
            SimpleLogger.Log("Timer started");
            timer.Start();
        }
        public void Stop() { 
            SimpleLogger.Log("Timer stopped");
            timer.Stop(); 
        }

        public void TimerTime_Tick(object sender, EventArgs e)
        {
            SimpleLogger.Log("Play music - TimerTime_Tick");
            if (_audioService.audioFile == null || !_audioService._isMusicPlaying)
                return;

            if (_audioService._outputDevice != null && _audioService._outputDevice.PlaybackState == PlaybackState.Playing)
            {
                double progress = _audioService.audioFile.CurrentTime.TotalSeconds / _audioService.audioFile.TotalTime.TotalSeconds;
                _viewModel.TrackPosition = progress * _audioService.startPage.MusicTrackBar.Maximum;
                _viewModel.CurrentTime = _audioService.audioFile.CurrentTime.ToString(@"mm\:ss");
                _viewModel.EndTime = _audioService.audioFile.TotalTime.ToString(@"mm\:ss");
            }
        }
    }
}

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
            timer.Start();
        }
        public void Stop() { 
            timer.Stop(); 
        }

        public void TimerTime_Tick(object sender, EventArgs e)
        {
            if (_audioService.AudioFile == null || !_audioService.IsMusicPlaying)
                return;

            if (_audioService.OutputDevice != null && _audioService.OutputDevice.PlaybackState == PlaybackState.Playing)
            {
                double progress = _audioService.AudioFile.CurrentTime.TotalSeconds / _audioService.AudioFile.TotalTime.TotalSeconds;
                _viewModel.TrackPosition = progress * _audioService.StartPage.MusicTrackBar.Maximum;
                _viewModel.CurrentTime = _audioService.AudioFile.CurrentTime.ToString(@"mm\:ss");
                _viewModel.EndTime = _audioService.AudioFile.TotalTime.ToString(@"mm\:ss");
            }
        }
    }
}

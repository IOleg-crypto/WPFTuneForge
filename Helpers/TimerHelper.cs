using System;
using System.Windows.Threading;
using NAudio.Wave;
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

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();

        public void TimerTime_Tick(object sender, EventArgs e)
        {
            if (_audioService._outputDevice == null || !_audioService._isMusicPlaying || _audioService._userIsDragging)
                return;

            if (_audioService._outputDevice.PlaybackState == PlaybackState.Playing)
            {
                double progress = _audioService.audioFile.CurrentTime.TotalSeconds /
                                  _audioService.audioFile.TotalTime.TotalSeconds;

                _viewModel.TrackPosition = progress * _audioService.startPage.MusicTrackBar.Maximum;
                _viewModel.CurrentTime = _audioService.audioFile.CurrentTime.ToString(@"mm\:ss");
                _viewModel.EndTime = _audioService.audioFile.TotalTime.ToString(@"mm\:ss");
            }
        }
    }
}

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private MusicViewModel _viewModel = new();
        

        public TimerHelper(TimeSpan interval)
        {
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
            if (_audioService._audioFile == null || !_audioService._isMusicPlaying || _audioService._userIsDragging)
                return;

            if (_audioService.waveOutEvent != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                double progress = _audioFile.CurrentTime.TotalSeconds / _audioFile.TotalTime.TotalSeconds;
                _viewModel.TrackPosition = progress * _startPage.MusicTrackBar.Maximum;

                _viewModel.CurrentTime = _audioFile.CurrentTime.ToString(@"mm\:ss");
                _viewModel.EndTime = _audioFile.TotalTime.ToString(@"mm\:ss");
            }
        }


    }
}

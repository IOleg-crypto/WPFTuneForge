using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfTuneForgePlayer.Model;

namespace WpfTuneForgePlayer.ViewModel
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        private string _artist;
        private string _title;
        private ImageSource _albumArt;
        private double _trackPosition;
        private string _currentTime = "00:00";
        private string _endTime = "00:00";

        public MusicViewModel()
        {
            InitCommands();
        }


        // Init Commands
        private void InitCommands()
        {
            PlayCommand = new RelayCommand(() => MainWindow?.OnClickMusic(this , null));
            //StopCommand = new RelayCommand(() => MainWindow?.StopMusic());
            //RepeatCommand = new RelayCommand(() => MainWindow?.RepeatMusic());
        }

        //Needed to get reference to MainWindow and get methods
        public MainWindow MainWindow { get; set; }

        // Properties to get and set on API dynamic
        public string Artist
        {
            get => _artist;
            set { _artist = value; OnPropertyChanged(nameof(Artist)); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public ImageSource AlbumArt
        {
            get => _albumArt;
            set { _albumArt = value; OnPropertyChanged(nameof(AlbumArt)); }
        }

        public double TrackPosition
        {
            get => _trackPosition;
            set { _trackPosition = value; OnPropertyChanged(nameof(TrackPosition)); }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set { _currentTime = value; OnPropertyChanged(nameof(CurrentTime)); }
        }

        public string EndTime
        {
            get => _endTime;
            set { _endTime = value; OnPropertyChanged(nameof(EndTime)); }

        }

        // Commands
        public ICommand PlayCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand RepeatCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

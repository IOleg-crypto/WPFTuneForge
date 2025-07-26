using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.ViewModel;
using WpfTuneForgePlayer.Views;
using WpfTuneForgePlayer.Helpers;


namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MusicViewModel();
            AudioService = new AudioService(ViewModel);
            AudioMetaService = new AudioMetaService(ViewModel);
            FavoriteSongs = new FavoriteSongs(ViewModel);

            DeviceOutputModel = new DeviceOutputModel(AudioService , ViewModel , AudioMetaService);
            ViewModel.MainWindow = this;
            //Needed to fix bug with automatic playback music
            ViewModel.DeviceOutputModel = DeviceOutputModel;
            AudioService.DeviceOutputModel = DeviceOutputModel;

            NavigateToStartPage();
            ActionHandle();
        }
    }
}

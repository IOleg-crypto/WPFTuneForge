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
using WpfTuneForgePlayer.Shader;


namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private MusicViewModel _viewModel;
        private StartPage _startPage;
        private AudioService _audioService;
        private AudioMetaService _audioMetaService;
        private DeviceOutputModel _deviceOutputModel;
        private FavoriteSongs _favoriteSongs;

        public MusicViewModel ViewModel
        {
            get => _viewModel;
            private set => _viewModel = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MusicViewModel();
            _startPage = new StartPage(ViewModel);
            _audioService = new AudioService(ViewModel);
            Sidebar.ViewModel = ViewModel;
            _audioMetaService = new AudioMetaService(ViewModel);
            _deviceOutputModel = new DeviceOutputModel(_audioService, ViewModel, _audioMetaService);
            ViewModel.MainWindow = this;
            ViewModel.DeviceOutputModel = _deviceOutputModel;
            _audioService.DeviceOutputModel = _deviceOutputModel;
            NavigateToStartPage();
            ActionHandle();
        }
    }
}

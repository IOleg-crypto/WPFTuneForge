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

            _viewModel = new MusicViewModel();
            audioService = new AudioService(_viewModel);
            audioMetaService = new AudioMetaService(_viewModel);

            _deviceOutputModel = new DeviceOutputModel();
            _viewModel.MainWindow = this;

            NavigateToStartPage();
            ActionHandle();
        }
    }
}

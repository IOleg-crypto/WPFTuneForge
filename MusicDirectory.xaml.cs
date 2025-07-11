using System;
using System.Collections.Generic;
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
using WpfTuneForgePlayer.ViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using WinForm = System.Windows.Forms;

namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for MusicDirectory.xaml
    /// </summary>
    public partial class MusicDirectory : Page
    {
        private StartPage _startPage;
        private MusicViewModel _viewModel;
        public MusicDirectory(MusicViewModel vm)
        {
            InitializeComponent();
            _startPage = new StartPage();
            _viewModel = vm;
            DataContext = vm;
        }
        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                _viewModel.MainWindow = mainWindow;
                _startPage.DataContext = _viewModel;
                mainWindow.MainContentFrame.Navigate(_startPage);
            }
        }

        private void OpenMusicFolder(object sender , RoutedEventArgs e)
        {
            WinForm.FolderBrowserDialog folderBrowserDialog = new WinForm.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                _viewModel.LoadSongs(folderBrowserDialog.SelectedPath);
            }
        }
    }
}

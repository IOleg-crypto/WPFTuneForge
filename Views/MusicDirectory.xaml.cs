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
using System.Collections.Specialized; 
using WpfTuneForgePlayer.Views;

namespace WpfTuneForgePlayer
{
    public partial class MusicDirectory : Page
    {
        private StartPage _startPage;
        private MusicViewModel _viewModel;

        public string CurrentDirectory
        {
            get; set;
        }

        public MusicDirectory(MusicViewModel vm)
        {
            InitializeComponent();
            _startPage = new StartPage();
            _viewModel = vm;
            DataContext = vm;
            

            if (_viewModel.Songs is INotifyCollectionChanged incc)
            {
                incc.CollectionChanged += Songs_CollectionChanged;
            }

            CheckCollectionSongs();
        }

        private void Songs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CheckCollectionSongs();
        }

        private void CheckCollectionSongs()
        {
            if (_viewModel.Songs != null && _viewModel.Songs.Count == 0)
            {
                InfoInDirectory.Visibility = Visibility.Visible;
            }
            else
            {
                InfoInDirectory.Visibility = Visibility.Collapsed;
            }
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

        private void OpenMusicFolder(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.LoadSongs(folderBrowserDialog.SelectedPath);
                _viewModel.TakeCurrentDirectory = folderBrowserDialog.SelectedPath;
            }
        }
    }
}


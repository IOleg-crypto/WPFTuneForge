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
        private readonly MusicViewModel _viewModel;

        public string CurrentDirectory { get; set; }

        public MusicDirectory(MusicViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "MusicViewModel не може бути null!");
            InitializeComponent();
            _viewModel = vm;
            DataContext = vm;

            SubscribeToCollectionChanges();
            UpdateInfoMessage();
        }

        private void SubscribeToCollectionChanges()
        {
            if (_viewModel.Songs is INotifyCollectionChanged incc)
            {
                incc.CollectionChanged += Songs_CollectionChanged;
            }
        }

        private void Songs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateInfoMessage();
        }


        private void UpdateInfoMessage()
        {
            if (_viewModel.Songs == null || _viewModel.Songs.Count == 0)
                InfoInDirectory.Visibility = Visibility.Visible;
            else
                InfoInDirectory.Visibility = Visibility.Collapsed;
        }

        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                _viewModel.MainWindow = mainWindow;
                mainWindow.MainContentFrame.Navigate(new StartPage(_viewModel));
            }
        }


        private void OpenMusicFolder(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    _viewModel.LoadSongs(selectedPath);
                    _viewModel.TakeCurrentDirectory = selectedPath;
                }
            }
        }
    }
}


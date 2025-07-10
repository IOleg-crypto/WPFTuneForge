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

namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for MusicDirectory.xaml
    /// </summary>
    public partial class MusicDirectory : Page
    {
        public MusicDirectory()
        {
            InitializeComponent();
        }
        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainContentFrame.Navigate(new StartPage());
            }
        }
    }
}

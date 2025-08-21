using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagLib.Mpeg;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.ViewModel;
using WpfTuneForgePlayer.Shader;


namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();

                BackgroundImageBlur.Loaded += (s, e) =>
                {
                    if (BlurShader is BlurEffect effect)
                    {
                        effect.PixelSize = new Point(1.0 / BackgroundImageBlur.ActualWidth,
                                                     1.0 / BackgroundImageBlur.ActualHeight);
                    }
                };

        }
    }
}

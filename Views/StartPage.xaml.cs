using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Drawing; // для Bitmap
using WpfTuneForgePlayer.Shader; // для GaussianBlur
using System.ComponentModel; // для PropertyChangedEventArgs
using WpfTuneForgePlayer.ViewModel; // для MusicViewModel
using System.Runtime.InteropServices; // для DeleteObject

namespace WpfTuneForgePlayer
{
    public partial class StartPage : Page
    {
        private BlurEffect _blurEffect;
        private MusicViewModel _viewModel;

        public StartPage(MusicViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetPageBackgroundBlur();

            if (BackgroundImageBlur?.Effect is BlurEffect blur)
            {
                _blurEffect = blur;
                AnimateBlur(0, 25, TimeSpan.FromSeconds(2));
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.AlbumArtPath))
            {
                SetPageBackgroundBlur();
            }
        }

        private void SetPageBackgroundBlur()
        {
            if (_viewModel.AlbumArt == null)
                return;

            BackgroundImageBlur.Source = _viewModel.AlbumArt;

            var blurEffect = new BlurEffect
            {
                Radius = 30,
                RenderingBias = RenderingBias.Quality
            };
            BackgroundImageBlur.Effect = blurEffect;
        }

        private void AnimateBlur(double from, double to, TimeSpan duration)
        {
            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(duration),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _blurEffect?.BeginAnimation(BlurEffect.RadiusProperty, animation);
        }

        public void SetBlurRadius(double radius)
        {
            if (_blurEffect != null)
                _blurEffect.Radius = radius;
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}

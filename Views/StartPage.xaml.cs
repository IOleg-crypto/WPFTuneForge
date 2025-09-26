using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Drawing; 
using WpfTuneForgePlayer.Shader;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer
{
    public partial class StartPage : Page
    {
        private BlurEffect _blurEffect;
        private MusicViewModel _viewModel;

        public StartPage(MusicViewModel viewModel)
        {
            InitializeComponent();

            SetPageBackgroundBlur(); 

            _viewModel = viewModel;

            if (BackgroundImageBlur?.Effect is BlurEffect blur)
            {
                _blurEffect = blur;
                AnimateBlur(0, 25, TimeSpan.FromSeconds(10));
            }
        }

        private void SetPageBackgroundBlur()
        {
            var imagePath = _viewModel.AlbumArt.ToString();
            if (!System.IO.File.Exists(imagePath))
                return;
            using (Bitmap bmp = new Bitmap(imagePath))
            {
                var blur = new GaussianBlur(bmp);
                Bitmap blurred = blur.Process(30); 


                var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    blurred.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                );

                BackgroundImageBlur.Source = bitmapSource;
            }
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
    }
}

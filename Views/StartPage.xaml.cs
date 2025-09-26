using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Drawing; // Додаємо для Bitmap
using WpfTuneForgePlayer.Shader; // Додаємо для GaussianBlur
using System.ComponentModel; // Додаємо для PropertyChangedEventArgs
using WpfTuneForgePlayer.ViewModel; // Додаємо для MusicViewModel

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
            var imagePath = _viewModel.AlbumArtPath ?? "assets/menu/musicLogo.jpg";
            if (string.IsNullOrEmpty(imagePath) || !System.IO.File.Exists(imagePath))
                return;
            using (Bitmap bmp = new Bitmap(imagePath))
            {
                // 2. Розмиття
                var blur = new GaussianBlur(bmp);
                Bitmap blurred = blur.Process(50); // 30 — радіус, можна змінити

                // 3. Перетворення у BitmapSource для WPF
                var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    blurred.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                );

                // 4. Встановлення як фон
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

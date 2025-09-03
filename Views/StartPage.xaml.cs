using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace WpfTuneForgePlayer
{
    public partial class StartPage : Page
    {
        private BlurEffect _blurEffect;

        public StartPage()
        {
            InitializeComponent();

            _blurEffect = (BlurEffect)BackgroundImageBlur.Effect;

            // Запускаємо анімацію blur
            AnimateBlur(0, 25, TimeSpan.FromSeconds(2));
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
            _blurEffect.BeginAnimation(BlurEffect.RadiusProperty, animation);
        }

        public void SetBlurRadius(double radius)
        {
            _blurEffect.Radius = radius;
        }
    }
}

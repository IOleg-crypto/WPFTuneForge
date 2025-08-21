using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfTuneForgePlayer.Shader {

    public class BlurEffect : ShaderEffect
    {
        private static readonly PixelShader _shader = new PixelShader
        {
            UriSource = new Uri("D:\\gitnext\\WpfTuneForgePlayer\\Shader\\GaussianBlur.ps")
        };

        public BlurEffect()
        {
            PixelShader = _shader;
            UpdateShaderValue(PixelSizeProperty);
        }

        public static readonly DependencyProperty PixelSizeProperty =
            DependencyProperty.Register("PixelSize", typeof(Point), typeof(BlurEffect),
                new UIPropertyMetadata(new Point(1.0 / 320, 1.0 / 240),
                    PixelShaderConstantCallback(0)));

        public Point PixelSize
        {
            get => (Point)GetValue(PixelSizeProperty);
            set => SetValue(PixelSizeProperty, value);
        }
    }
}
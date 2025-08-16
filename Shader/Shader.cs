using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using System.IO;

namespace WpfTuneForgePlayer.Shader
{
    public class Shader : ShaderEffect
    {
        private static readonly PixelShader _shader = new PixelShader
        {
            UriSource = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory , "Shader/GaussianBlur.ps"), UriKind.Relative)
        };

        public Shader()
        {
            PixelShader = _shader;
            UpdateShaderValue(RadiusProperty);
            UpdateShaderValue(PixelSizeProperty);
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(Shader),
                new UIPropertyMetadata(5.0, PixelShaderConstantCallback(0)));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public static readonly DependencyProperty PixelSizeProperty =
            DependencyProperty.Register("PixelSize", typeof(Point), typeof(Shader),
                new UIPropertyMetadata(new Point(0.001, 0.001), PixelShaderConstantCallback(1)));

        public Point PixelSize
        {
            get => (Point)GetValue(PixelSizeProperty);
            set => SetValue(PixelSizeProperty, value);
        }
    }
}

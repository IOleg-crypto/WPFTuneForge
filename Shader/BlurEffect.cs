
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using GLWpfControl;

namespace WpfTuneForgePlayer.Shader
{
    public class BlurEffect : ShaderEffect
    {
        private static readonly PixelShader _shader = new PixelShader()
        {
            UriSource = new Uri("pack://application:,,,/GaussianBlur.ps")
        };

        public BlurEffect()
        {
            PixelShader = _shader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ResolutionProperty);
            UpdateShaderValue(RadiusProperty);
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BlurEffect), 0);

        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public static readonly DependencyProperty ResolutionProperty =
            DependencyProperty.Register("Resolution", typeof(Point), typeof(BlurEffect),
                new UIPropertyMetadata(new Point(800, 600), PixelShaderConstantCallback(0)));

        public Point Resolution
        {
            get => (Point)GetValue(ResolutionProperty);
            set => SetValue(ResolutionProperty, value);
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(BlurEffect),
                new UIPropertyMetadata(2.0, PixelShaderConstantCallback(1)));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
    }
}


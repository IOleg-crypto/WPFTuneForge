
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfTuneForgePlayer.Shader
{
    public class BlurEffect : ShaderEffect
    {
        public BlurEffect()
        {
            PixelShader pixelShader = new PixelShader();

            // Завантаження з диску через потік
            using (FileStream fs = new FileStream(@"D:\gitnext\WpfTuneForgePlayer\Shader\GaussianBlur.ps", FileMode.Open, FileAccess.Read))
            {
                pixelShader.SetStreamSource(fs);
            }

            PixelShader = pixelShader;

            // Після присвоєння PixelShader можна викликати UpdateShaderValue
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


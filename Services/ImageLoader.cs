using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfTuneForgePlayer.ViewModel
{
    public class ImageLoader
    {
        // Load an image from file or return null if not found
        public static ImageSource LoadImageOrDefault(string relativePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (!File.Exists(fullPath))
                return null;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(fullPath, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze(); // Makes it cross-thread accessible
            return image;
        }

        // Extract album art from a TagLib file
        public static ImageSource LoadAlbumArt(TagLib.File tagFile)
        {
            if (tagFile.Tag.Pictures.Length == 0) return null;

            var bin = tagFile.Tag.Pictures[0].Data.Data;
            using var ms = new MemoryStream(bin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}

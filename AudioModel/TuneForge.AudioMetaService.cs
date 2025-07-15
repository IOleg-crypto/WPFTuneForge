using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfTuneForgePlayer.ViewModel;

namespace WpfTuneForgePlayer.AudioModel
{
    public class AudioMetaService
    {
        private MusicViewModel _viewModel;

        public AudioMetaService(MusicViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        // Extract album art from music file using TagLib
        private BitmapImage GetAlbumArt(string path)
        {
            if (!File.Exists(path)) return null;

            using var tagFile = TagLib.File.Create(path);
            if (tagFile.Tag.Pictures.Length == 0) return null;

            var bin = tagFile.Tag.Pictures[0].Data.Data;
            if (bin.Length == 0) return null;

            using var ms = new MemoryStream(bin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }

        // Update album art image in ViewModel
        public void UpdateAlbumArt(string path)
        {
            var albumImage = GetAlbumArt(path);
            if (albumImage == null) return;

            _viewModel.AlbumArt = albumImage;
        }

        // Extract artist and song title using TagLib (fallback to filename parsing if tags are empty)
        public void TakeArtistSongName(string path)
        {
            try
            {
                var file = TagLib.File.Create(path);
                string artist = file.Tag.FirstPerformer ?? string.Empty;
                string title = file.Tag.Title ?? string.Empty;

                if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(title))
                {
                    var fileName = Path.GetFileNameWithoutExtension(path);
                    var parts = fileName.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        artist = parts[0].Trim();
                        title = parts[1].Trim();
                    }
                    else
                    {
                        title = fileName;
                    }
                }

                _viewModel.Artist = artist;
                _viewModel.Title = title;
            }
            catch
            {
                _viewModel.Artist = "";
                _viewModel.Title = "";
            }
        }
    }
}

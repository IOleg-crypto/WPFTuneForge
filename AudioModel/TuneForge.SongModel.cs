using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfTuneForgePlayer.AudioModel
{
     public class SongModel
     {
          public string Title { get; set; }
          public string Artist { get; set; }
          public string Duration { get; set; }
          public ImageSource AlbumArt { get; set; }
          public string FilePath { get; set; }
     }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTuneForgePlayer.Helpers
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Duration { get; set; }

        public Song(string title, string artist, string duration)
        {
            Title = title;
            Artist = artist;
            Duration = duration;
        }
    }
}

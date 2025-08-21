using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTuneForgePlayer.Helpers
{
    public class Song : IEquatable<Song>
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

        public bool Equals(Song other)
        {
            if (other is null) return false;
            return Title == other.Title
                && Artist == other.Artist
                && Duration == other.Duration;
        }

        public override bool Equals(object obj) => Equals(obj as Song);

        public override int GetHashCode() =>
            HashCode.Combine(Title, Artist, Duration);

        public static bool operator ==(Song left, Song right) => Equals(left, right);
        public static bool operator !=(Song left, Song right) => !Equals(left, right);
    }
}

using WpfTuneForgePlayer.Helpers;

namespace WpfTuneForge.Tests
{
    public class UnitTest2
    {
        [Theory]
        [InlineData("Test Song", "Test Artist", "03:45")]
        [InlineData("", "", "")]
        [InlineData(null, null, null)]
        public void Constructor_ShouldSetProperties(string title, string artist, string duration)
        {
            var song = new Song(title, artist, duration);
            Assert.Equal(title, song.Title);
            Assert.Equal(artist, song.Artist);
            Assert.Equal(duration, song.Duration);
        }

        [Theory]
        // equal
        [InlineData("A", "B", "01:00", "A", "B", "01:00", true)]
        // different title
        [InlineData("A", "B", "01:00", "C", "B", "01:00", false)]
        // different artist
        [InlineData("A", "B", "01:00", "A", "X", "01:00", false)]
        // different duration
        [InlineData("A", "B", "01:00", "A", "B", "02:00", false)]
        public void Equals_ValueComparison(string t1, string a1, string d1, string t2, string a2, string d2, bool expected)
        {
            var s1 = new Song(t1, a1, d1);
            var s2 = new Song(t2, a2, d2);
            Assert.Equal(expected, s1.Equals(s2));
            Assert.Equal(expected, s2.Equals(s1));
        }

        [Fact]
        public void Equals_Null_ReturnsFalse()
        {
            var song = new Song("A", "B", "01:00");
            Assert.False(song.Equals(null));
        }

        [Fact]
        public void Equals_SameReference_ReturnsTrue()
        {
            var song = new Song("A", "B", "01:00");
            Assert.True(song.Equals(song));
        }

        [Theory]
        // equal objects => same hash
        [InlineData("A", "B", "01:00", "A", "B", "01:00", true)]
        // different objects => hash may differ
        [InlineData("A", "B", "01:00", "C", "B", "01:00", false)]
        public void GetHashCode_Consistency(string t1, string a1, string d1, string t2, string a2, string d2, bool same)
        {
            var s1 = new Song(t1, a1, d1);
            var s2 = new Song(t2, a2, d2);
            if (same)
            {
                Assert.Equal(s1.GetHashCode(), s2.GetHashCode());
            }
            else
            {
                Assert.NotEqual(s1.GetHashCode(), s2.GetHashCode());
            }
        }

        [Theory]
        [InlineData("A", "B", "01:00", "A", "B", "01:00", true)]
        [InlineData("A", "B", "01:00", "X", "B", "01:00", false)]
        public void EqualityOperator_Works(string t1, string a1, string d1, string t2, string a2, string d2, bool expected)
        {
            var s1 = new Song(t1, a1, d1);
            var s2 = new Song(t2, a2, d2);
            Assert.Equal(expected, s1 == s2);
            Assert.Equal(!expected, s1 != s2);
        }
    }
}


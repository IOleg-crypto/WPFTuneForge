using System;
using Xunit;
using WpfTuneForgePlayer.Helpers;

namespace WpfTuneForgePlayer.Tests.Helpers
{
    public class SongTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateSong()
        {
            // Arrange
            string title = "Test Song";
            string artist = "Test Artist";
            string duration = "03:45";

            // Act
            var song = new Song(title, artist, duration);

            // Assert
            Assert.Equal(title, song.Title);
            Assert.Equal(artist, song.Artist);
            Assert.Equal(duration, song.Duration);
        }

        [Fact]
        public void Constructor_WithNullParameters_ShouldCreateSong()
        {
            // Act
            var song = new Song(null, null, null);

            // Assert
            Assert.Null(song.Title);
            Assert.Null(song.Artist);
            Assert.Null(song.Duration);
        }

        [Fact]
        public void Equals_WithSameProperties_ShouldReturnTrue()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Test Song", "Test Artist", "03:45");

            // Act
            bool result = song1.Equals(song2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentProperties_ShouldReturnFalse()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Different Song", "Test Artist", "03:45");

            // Act
            bool result = song1.Equals(song2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var song = new Song("Test Song", "Test Artist", "03:45");

            // Act
            bool result = song.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithSameReference_ShouldReturnTrue()
        {
            // Arrange
            var song = new Song("Test Song", "Test Artist", "03:45");

            // Act
            bool result = song.Equals(song);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetHashCode_WithSameProperties_ShouldReturnSameHashCode()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Test Song", "Test Artist", "03:45");

            // Act
            int hashCode1 = song1.GetHashCode();
            int hashCode2 = song2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_WithDifferentProperties_ShouldReturnDifferentHashCode()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Different Song", "Test Artist", "03:45");

            // Act
            int hashCode1 = song1.GetHashCode();
            int hashCode2 = song2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void EqualityOperator_WithSameProperties_ShouldReturnTrue()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Test Song", "Test Artist", "03:45");

            // Act
            bool result = song1 == song2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_WithDifferentProperties_ShouldReturnFalse()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Different Song", "Test Artist", "03:45");

            // Act
            bool result = song1 == song2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_WithSameProperties_ShouldReturnFalse()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Test Song", "Test Artist", "03:45");

            // Act
            bool result = song1 != song2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_WithDifferentProperties_ShouldReturnTrue()
        {
            // Arrange
            var song1 = new Song("Test Song", "Test Artist", "03:45");
            var song2 = new Song("Different Song", "Test Artist", "03:45");

            // Act
            bool result = song1 != song2;

            // Assert
            Assert.True(result);
        }
    }
}

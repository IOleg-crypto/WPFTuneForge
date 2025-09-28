using System;
using Xunit;
using Mathematics;

namespace WpfTuneForgePlayer.Tests.Mathematics
{
    public class Vector2Tests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateVector2()
        {
            // Arrange
            object width = 100;
            object height = 200;

            // Act
            var vector = new Vector2(width, height);

            // Assert
            Assert.NotNull(vector);
        }

        [Fact]
        public void Constructor_WithNullParameters_ShouldCreateVector2()
        {
            // Act
            var vector = new Vector2(null, null);

            // Assert
            Assert.NotNull(vector);
        }

        [Fact]
        public void Constructor_WithMixedTypes_ShouldCreateVector2()
        {
            // Arrange
            object width = "100";
            object height = 200.5;

            // Act
            var vector = new Vector2(width, height);

            // Assert
            Assert.NotNull(vector);
        }
    }
}

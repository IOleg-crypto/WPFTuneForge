using System;
using System.IO;
using Xunit;
using Moq;
using WpfTuneForgePlayer.AudioModel;
using WpfTuneForgePlayer.ViewModel;
using WpfTuneForgePlayer.Helpers;
using System.Collections.ObjectModel;

namespace WpfTuneForgePlayer.Tests.Services
{
    public class AudioServiceTests
    {
        private Mock<MusicViewModel> _mockViewModel;
        private AudioService _audioService;

        public AudioServiceTests()
        {
            _mockViewModel = new Mock<MusicViewModel>();
            _audioService = new AudioService(_mockViewModel.Object);
        }

        [Fact]
        public void Constructor_WithValidViewModel_ShouldCreateAudioService()
        {
            // Act
            var audioService = new AudioService(_mockViewModel.Object);

            // Assert
            Assert.NotNull(audioService);
            Assert.NotNull(audioService.VolumeService);
            Assert.NotNull(audioService.MusicNavigationService);
        }

        [Fact]
        public void Constructor_WithNullViewModel_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AudioService(null));
        }

        [Fact]
        public void CurrentMusicPath_Get_ShouldReturnEmptyStringWhenNull()
        {
            // Arrange
            _audioService.CurrentMusicPath = null;

            // Act
            string result = _audioService.CurrentMusicPath;

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CurrentMusicPath_SetAndGet_ShouldReturnSetValue()
        {
            // Arrange
            string expectedPath = @"C:\Music\song.mp3";

            // Act
            _audioService.CurrentMusicPath = expectedPath;
            string result = _audioService.CurrentMusicPath;

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Fact]
        public void NewMusicPath_Get_ShouldReturnEmptyStringWhenNull()
        {
            // Arrange
            _audioService.NewMusicPath = null;

            // Act
            string result = _audioService.NewMusicPath;

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void NewMusicPath_SetAndGet_ShouldReturnSetValue()
        {
            // Arrange
            string expectedPath = @"C:\Music\newsong.mp3";

            // Act
            _audioService.NewMusicPath = expectedPath;
            string result = _audioService.NewMusicPath;

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Fact]
        public void IsSound_SetAndGet_ShouldReturnSetValue()
        {
            // Act
            _audioService.IsSound = true;
            bool result = _audioService.IsSound;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSelectedSongFavorite_SetAndGet_ShouldReturnSetValue()
        {
            // Act
            _audioService.IsSelectedSongFavorite = true;
            bool result = _audioService.IsSelectedSongFavorite;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSliderEnabled_SetAndGet_ShouldReturnSetValue()
        {
            // Act
            _audioService.IsSliderEnabled = true;
            bool result = _audioService.IsSliderEnabled;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsMusicPlaying_SetAndGet_ShouldReturnSetValue()
        {
            // Act
            _audioService.IsMusicPlaying = true;
            bool result = _audioService.IsMusicPlaying;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsManualStop_SetAndGet_ShouldReturnSetValue()
        {
            // Act
            _audioService.IsManualStop = true;
            bool result = _audioService.IsManualStop;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SliderChanged_WithNullAudioFile_ShouldReturnEarly()
        {
            // Arrange
            _audioService.AudioFile = null;
            _mockViewModel.Setup(x => x.TrackPosition).Returns(5000);
            _mockViewModel.Setup(x => x.TrackBarMaximum).Returns(10000);

            // Act & Assert - Should not throw exception
            _audioService.SliderChanged();
            Assert.True(true); // If we reach here, method handled null gracefully
        }

        [Fact]
        public void SliderChanged_WithNullOutputDevice_ShouldReturnEarly()
        {
            // Arrange
            _audioService.OutputDevice = null;
            _mockViewModel.Setup(x => x.TrackPosition).Returns(5000);
            _mockViewModel.Setup(x => x.TrackBarMaximum).Returns(10000);

            // Act & Assert - Should not throw exception
            _audioService.SliderChanged();
            Assert.True(true); // If we reach here, method handled null gracefully
        }

        // Test for SaveFavoriteSongs method
        [Fact]
        public void SaveFavoriteSongs_WithNullCollection_ShouldReturnEarly()
        {
            // Act & Assert - Should not throw exception
            _audioService.SaveFavoriteSongs(null);
            Assert.True(true); // If we reach here, method handled null gracefully
        }

        [Fact]
        public void SaveFavoriteSongs_WithEmptyCollection_ShouldReturnEarly()
        {
            // Arrange
            var emptyCollection = new ObservableCollection<Song>();

            // Act & Assert - Should not throw exception
            _audioService.SaveFavoriteSongs(emptyCollection);
            Assert.True(true); // If we reach here, method handled empty collection gracefully
        }

        // Integration test placeholder - would require actual audio files
        [Fact(Skip = "Requires audio hardware and files - run manually")]
        public void OnClickMusic_WithValidFile_ShouldStartPlayback()
        {
            // This test would require actual audio files and hardware
            // It's marked as skipped to prevent CI/CD failures
            Assert.True(true);
        }

        [Fact(Skip = "Requires audio hardware and files - run manually")]
        public void StopAndDisposeCurrentMusic_ShouldStopPlayback()
        {
            // This test would require actual audio files and hardware
            // It's marked as skipped to prevent CI/CD failures
            Assert.True(true);
        }
    }
}

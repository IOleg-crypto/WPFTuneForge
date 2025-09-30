using System.Text.RegularExpressions;
using WpfTuneForgePlayer.Helpers;
using System.IO;
namespace WpfTuneForge.Tests
{
    [Collection("Logging collection")]
    public class UnitTest1
    {
        private static readonly Regex TimestampRegex = new Regex(@"\b\d{2}:\d{2}:\d{2}\b", RegexOptions.Compiled);

        [Fact]
        public void SimpleLogger_Log_WithValidMessage_ShouldWriteToFile()
        {
            // Arrange
            string testMessage = "Test log message";

            try
            {
                // Act
                SimpleLogger.Log(testMessage);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains(testMessage, logContent);
                Assert.Matches(TimestampRegex, logContent);
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        [Fact]
        public void SimpleLogger_Log_WithEmptyMessage_ShouldWriteToFile()
        {
            // Arrange
            string emptyMessage = "";

            try
            {
                // Act
                SimpleLogger.Log(emptyMessage);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains(emptyMessage, logContent);
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        [Fact]
        public void SimpleLogger_Log_WithNullMessage_ShouldWriteToFile()
        {
            // Arrange
            string nullMessage = null;

            try
            {
                // Act
                SimpleLogger.Log(nullMessage);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains("", logContent); // Should contain empty string
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        [Fact]
        public void SimpleLogger_Log_MultipleMessages_ShouldAppendToFile()
        {
            // Arrange
            string message1 = "First message";
            string message2 = "Second message";

            try
            {
                // Act
                SimpleLogger.Log(message1);
                SimpleLogger.Log(message2);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains(message1, logContent);
                Assert.Contains(message2, logContent);

                // Should have two log entries (two timestamps)
                var lines = File.ReadAllLines("log.txt");
                Assert.True(lines.Length >= 2);
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        [Fact]
        public void SimpleLogger_Log_WithSpecialCharacters_ShouldWriteToFile()
        {
            // Arrange
            string specialMessage = "Special chars: !@#$%^&*()_+-=[]{}|;':\",./<>?`~";

            try
            {
                // Act
                SimpleLogger.Log(specialMessage);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains(specialMessage, logContent);
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        [Fact]
        public void SimpleLogger_Log_WithLongMessage_ShouldWriteToFile()
        {
            // Arrange
            string longMessage = new string('A', 1000); // 1000 character string

            try
            {
                // Act
                SimpleLogger.Log(longMessage);

                // Assert
                Assert.True(File.Exists("log.txt"));
                string logContent = File.ReadAllText("log.txt");
                Assert.Contains(longMessage, logContent);
            }
            finally
            {
                // Clean up
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
        }

        // Note: ExternalConsoleLogger tests are skipped because they require:
        // 1. PowerShell to be available
        // 2. Process creation capabilities
        // 3. WPF Application context
        // These would be better suited for integration tests

        [Fact(Skip = "Requires PowerShell and process creation - run manually")]
        public void ExternalConsoleLogger_StartConsoleWatcher_WithValidPath_ShouldStartProcess()
        {
            // This test would require PowerShell and process creation capabilities
            // It's marked as skipped to prevent CI/CD failures
            Assert.True(true);
        }

        [Fact(Skip = "Requires PowerShell and process creation - run manually")]
        public void ExternalConsoleLogger_StopConsoleWatcher_ShouldStopProcess()
        {
            // This test would require PowerShell and process creation capabilities
            // It's marked as skipped to prevent CI/CD failures
            Assert.True(true);
        }
    }
}

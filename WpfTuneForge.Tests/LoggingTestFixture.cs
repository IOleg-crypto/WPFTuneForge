using System;
using System.IO;

namespace WpfTuneForge.Tests
{
    // Disables parallelization for any tests in this collection and provides
    // an isolated temporary working directory for file-system based tests.
    [CollectionDefinition("Logging collection", DisableParallelization = true)]
    public class LoggingCollection : ICollectionFixture<LoggingTestFixture>
    {
    }

    public sealed class LoggingTestFixture : IDisposable
    {
        private readonly string originalWorkingDirectory;
        public string TempWorkingDirectory { get; }

        public LoggingTestFixture()
        {
            originalWorkingDirectory = Environment.CurrentDirectory;
            TempWorkingDirectory = Path.Combine(Path.GetTempPath(),
                $"WpfTuneForge.Tests_{Guid.NewGuid():N}");
            Directory.CreateDirectory(TempWorkingDirectory);
            Environment.CurrentDirectory = TempWorkingDirectory;
        }

        public void Dispose()
        {
            try
            {
                Environment.CurrentDirectory = originalWorkingDirectory;
                if (Directory.Exists(TempWorkingDirectory))
                {
                    Directory.Delete(TempWorkingDirectory, true);
                }
            }
            catch
            {
                // Best-effort cleanup; ignore failures in test teardown
            }
        }
    }
}



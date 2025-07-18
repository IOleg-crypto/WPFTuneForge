using System;
using System.Diagnostics;
using System.IO;

namespace WpfTuneForgePlayer.Helpers
{
    public static class ExternalConsoleLogger
    {
        private static Process consoleProcess;

        public static void StartConsoleWatcher(string logFilePath)
        {
            if (consoleProcess != null && !consoleProcess.HasExited)
                return; 

            if (!File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, "Log start\n");
            }

            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoExit -Command \"Get-Content -Path '{logFilePath}' -Wait\"",
                UseShellExecute = true,
                CreateNoWindow = false
            };

            consoleProcess = Process.Start(psi);
        }

        public static Process ConsoleProcess => consoleProcess;

        public static void StopConsoleWatcher()
        {
            if (consoleProcess != null && !consoleProcess.HasExited)
            {
                consoleProcess.Kill();
                consoleProcess.Dispose();
                consoleProcess = null;
            }
        }
    }
    public static class SimpleLogger
    {
        private static readonly string logFile = "log.txt";
        private static bool isFirstLog = true;

        public static void Log(string message)
        {
            try
            {
                using (var writer = new StreamWriter(logFile, !isFirstLog)) // true = append, false = overwrite
                {
                    string logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}";
                    writer.WriteLine(logEntry);
                }

                if (isFirstLog)
                    isFirstLog = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}

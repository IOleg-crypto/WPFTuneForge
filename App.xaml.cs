using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfTuneForgePlayer.Helpers;
using System.IO;

namespace WpfTuneForgePlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            File.WriteAllText("log.txt", string.Empty);
#if DEBUG
            ExternalConsoleLogger.StartConsoleWatcher("log.txt");
#endif
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTuneForgePlayer.AudioModel
{
    public class AudioDeviceInfo
    {
        public string Name { get; set; }
        public int Channels { get; set; }
        public int SampleRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.EventModels
{
    public class InstanceSettingsEvent
    {
        public string CurrentInstance { get; set; }
        public bool LauncherOpen { get; set; }
        public string Arguments { get; set; }
        public int GameWidth { get; set; }
        public int GameHeight { get; set; }
    }
}

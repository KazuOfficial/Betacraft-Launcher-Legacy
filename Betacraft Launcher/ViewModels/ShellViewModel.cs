using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\betacraftlegacy\versions\jsons\";
        public ShellViewModel()
        {
            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());
            //ActivateItemAsync(IoC.Get<VersionViewModel>(), new CancellationToken());

            if (!Directory.Exists(launcherPath))
            {
                Directory.CreateDirectory(launcherPath);
            }
        }
    }
}

using BetacraftLauncher.Library;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class LauncherViewModel : Conductor<object>
    {
        private readonly IWindowManager windowManager;
        private readonly VersionViewModel versionVM;

        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
        }

        public async Task VersionList()
        {
            await this.windowManager.ShowDialogAsync(this.versionVM);
        }
    }
}

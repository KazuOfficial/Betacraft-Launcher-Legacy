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
        private readonly IDownloadVersionEndpoint dwVersionEndpoint;

        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM, IDownloadVersionEndpoint dwVersionEndpoint)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
            this.dwVersionEndpoint = dwVersionEndpoint;
        }

        public async Task VersionList()
        {
            await this.windowManager.ShowDialogAsync(this.versionVM);
        }

        public async Task Play()
        {
            await this.dwVersionEndpoint.DownloadVersion("1.0.0-rc2-1633");
        }
    }
}

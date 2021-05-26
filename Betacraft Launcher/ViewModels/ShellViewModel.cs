using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IFileInit fileInit;

        public ShellViewModel(IFileInit fileInit)
        {
            this.fileInit = fileInit;

            fileInit.FileInitialization();

            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());
            //ActivateItemAsync(IoC.Get<VersionViewModel>(), new CancellationToken());
        }
    }
}

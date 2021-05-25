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
        private readonly IEventAggregator events;
        private readonly IFileInit fileInit;

        public ShellViewModel(IEventAggregator events, IFileInit fileInit)
        {
            this.events = events;
            this.fileInit = fileInit;

            fileInit.FileInitialization();
            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());
            //ActivateItemAsync(IoC.Get<VersionViewModel>(), new CancellationToken());
        }
    }
}

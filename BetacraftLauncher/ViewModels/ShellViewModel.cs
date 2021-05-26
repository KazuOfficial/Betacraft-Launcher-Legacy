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
        private readonly IDiscordRPCManager discordRPC;

        public ShellViewModel(IFileInit fileInit, IDiscordRPCManager discordRPC)
        {
            this.fileInit = fileInit;
            this.discordRPC = discordRPC;

            fileInit.FileInitialization();

            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());

            if (Properties.Settings.Default.discordRPC)
            {
                discordRPC.Initialize();
            }
        }
    }
}

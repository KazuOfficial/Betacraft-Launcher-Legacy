using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IDiscordRPCManager discordRPC;
        private readonly ILog logger;

        public ShellViewModel(IDiscordRPCManager discordRPC, ILog logger)
        {
            this.discordRPC = discordRPC;
            this.logger = logger;

            logger.Info("Betacraft Launcher Legacy started!");

            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());

            if (Properties.Settings.Default.discordRPC)
            {
                discordRPC.Initialize();
            }
        }
    }
}

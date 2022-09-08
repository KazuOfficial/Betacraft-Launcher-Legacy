using BetacraftLauncher.Library.Interfaces;
using Caliburn.Micro;
using System.Threading;

namespace BetacraftLauncher.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IDiscordRPCManager _discordRPC;
        private readonly ILog _logger;

        public ShellViewModel(IDiscordRPCManager discordRPC, ILog logger)
        {
            _discordRPC = discordRPC;
            _logger = logger;

            _logger.Info("Betacraft Launcher Legacy started");

            ActivateItemAsync(IoC.Get<LauncherViewModel>(), new CancellationToken());

            if (Properties.Settings.Default.discordRPC)
            {
                _discordRPC.Initialize();
            }
        }
    }
}

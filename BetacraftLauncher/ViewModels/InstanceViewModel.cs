using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library.Interfaces;
using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class InstanceViewModel : Screen
    {
        private string launcherPath = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy";

        private readonly IEventAggregator _events;
        private readonly IDiscordRPCManager _discordRPC;
        private readonly ILog _logger;

        public InstanceViewModel(IEventAggregator events, IDiscordRPCManager discordRPC, ILog logger)
        {
            _events = events;
            _discordRPC = discordRPC;
            _logger = logger;

            LoadSettings();
        }

        private string _instanceName;

        public string InstanceName
        {
            get { return _instanceName; }
            set
            { 
                _instanceName = value;
                NotifyOfPropertyChange(() => InstanceName);
            }
        }

        private bool _launcherOpen;

        public bool LauncherOpen
        {
            get { return _launcherOpen; }
            set
            { 
                _launcherOpen = value;
                NotifyOfPropertyChange(() => LauncherOpen);
            }
        }

        private bool _rpc;

        public bool RPC
        {
            get { return _rpc; }
            set
            {
                _rpc = value;
                NotifyOfPropertyChange(() => RPC);
            }
        }

        private string _arguments;

        public string Arguments
        {
            get { return _arguments; }
            set
            { 
                _arguments = value;
                NotifyOfPropertyChange(() => Arguments);
            }
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set
            { 
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        private int _height;

        public int Height
        {
            get { return _height; }
            set
            { 
                _height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _logger.Info("InstanceViewModel started");
        }

        public void DirectoryButton()
        {
            Process.Start("explorer.exe", launcherPath);
            _logger.Info($"DirectoryButton clicked: {launcherPath}");
        }

        public async Task SubmitSettings()
        {
            SaveSettings();

            if (RPC == false)
            {
                _discordRPC.Deinitialize();
            }
            else
            {
                _discordRPC.Initialize();
                _logger.Info("Discord RPC Initialized");
            }

            await _events.PublishOnUIThreadAsync(new InstanceSettingsEvent { 
                CurrentInstance = InstanceName,
                LauncherOpen = LauncherOpen,
                Arguments = Arguments,
                GameWidth = Width,
                GameHeight = Height
            });

            _logger.Info("Event InstanceSettingsEvent sent");

            await TryCloseAsync();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.instanceName = InstanceName;
            Properties.Settings.Default.keepLauncherOpen = LauncherOpen;
            Properties.Settings.Default.discordRPC = RPC;
            Properties.Settings.Default.jvmArguments = Arguments;
            Properties.Settings.Default.width = Width;
            Properties.Settings.Default.height = Height;
            Properties.Settings.Default.Save();

            _logger.Info("InstanceViewModel settings saved");
        }

        private void LoadSettings()
        {
            InstanceName = Properties.Settings.Default.instanceName;
            LauncherOpen = Properties.Settings.Default.keepLauncherOpen;
            RPC = Properties.Settings.Default.discordRPC;
            Arguments = Properties.Settings.Default.jvmArguments;
            Width = Properties.Settings.Default.width;
            Height = Properties.Settings.Default.height;

            _logger.Info("Settings loaded to InstanceViewModel");
        }
    }
}

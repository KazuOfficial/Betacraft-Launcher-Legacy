using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class InstanceViewModel : Screen
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy";

        private readonly IEventAggregator events;
        private readonly IDiscordRPCManager discordRPC;

        public InstanceViewModel(IEventAggregator events, IDiscordRPCManager discordRPC)
        {
            LoadSettings();
            this.events = events;
            this.discordRPC = discordRPC;
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

        public void DirectoryButton()
        {
            Process.Start("explorer.exe", launcherPath);
        }

        public async Task SubmitSettings()
        {
            SaveSettings();

            if (RPC == false)
            {
                //discordRPC.Deinitialize();
            }
            else
            {
                discordRPC.Initialize();
            }

            await events.PublishOnUIThreadAsync(new InstanceSettingsEvent { 
                CurrentInstance = InstanceName,
                LauncherOpen = LauncherOpen,
                Arguments = Arguments,
                GameWidth = Width,
                GameHeight = Height
            });

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
        }

        private void LoadSettings()
        {
            InstanceName = Properties.Settings.Default.instanceName;
            LauncherOpen = Properties.Settings.Default.keepLauncherOpen;
            RPC = Properties.Settings.Default.discordRPC;
            Arguments = Properties.Settings.Default.jvmArguments;
            Width = Properties.Settings.Default.width;
            Height = Properties.Settings.Default.height;
        }
    }
}

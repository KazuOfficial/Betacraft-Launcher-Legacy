using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetacraftLauncher.ViewModels
{
    public class LauncherViewModel : Screen, IHandle<SelectVersionEvent>
    {
        private readonly IWindowManager windowManager;
        private readonly VersionViewModel versionVM;
        private readonly IDownloadVersionEndpoint dwVersionEndpoint;
        private readonly ILaunchManager launchManager;
        private readonly IEventAggregator events;
        private string _nickname;

        public string Nickname
        {
            get { return _nickname; }
            set 
            { 
                _nickname = value;
                NotifyOfPropertyChange(() => Nickname);
            }
        }

        private string _currentVersion;

        public string CurrentVersion
        {
            get { return _currentVersion; }
            set
            { 
                _currentVersion = value;
                NotifyOfPropertyChange(() => CurrentVersion);
            }
        }


        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM, IDownloadVersionEndpoint dwVersionEndpoint, ILaunchManager launchManager, IEventAggregator events)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
            this.dwVersionEndpoint = dwVersionEndpoint;
            this.launchManager = launchManager;
            this.events = events;

            events.Subscribe(this);

            if (Properties.Settings.Default.Nickname is not null)
            {
                Nickname = Properties.Settings.Default.Nickname;
            }

            if (Properties.Settings.Default is not null)
            {
                CurrentVersion = Properties.Settings.Default.lastInstance;
            }
        }

        public async Task VersionList()
        {
            await this.windowManager.ShowDialogAsync(this.versionVM);
        }

        public async Task Play()
        {
            if (Properties.Settings.Default.Nickname != Nickname)
            {
                Properties.Settings.Default.Nickname = Nickname;
                Properties.Settings.Default.Save();
            }

            await this.dwVersionEndpoint.DownloadVersion(CurrentVersion);

            await launchManager.LaunchGame(CurrentVersion, Nickname, "kz");

            Environment.Exit(0);
        }

        public void AuthorsGithub()
        {
            Process.Start(new ProcessStartInfo("https://github.com/KazuOfficial") { 
                UseShellExecute = true
            });
        }

        public async Task HandleAsync(SelectVersionEvent message, CancellationToken cancellationToken)
        {
            CurrentVersion = message.CurrentVersionMessage;
            NotifyOfPropertyChange(() => CurrentVersion);
        }
    }
}

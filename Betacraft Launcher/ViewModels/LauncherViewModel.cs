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
        private readonly LanguageViewModel languageVM;
        private readonly IDownloadVersionEndpoint dwVersionEndpoint;
        private readonly ILaunchManager launchManager;
        private readonly IEventAggregator events;

        private bool clickedPlay { get; set; }

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


        public bool CanPlay
        {
            get
            {
                bool output = true;
                if (clickedPlay)
                {
                    output = false;
                }

                return output;
            }
        }

        private Uri _browser;

        public Uri Browser
        {
            get { return _browser; }
            set
            {
                _browser = value;
                NotifyOfPropertyChange(() => Browser);
            }
        }


        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM, LanguageViewModel languageVM, IDownloadVersionEndpoint dwVersionEndpoint, ILaunchManager launchManager, IEventAggregator events)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
            this.languageVM = languageVM;
            this.dwVersionEndpoint = dwVersionEndpoint;
            this.launchManager = launchManager;
            this.events = events;

            events.Subscribe(this);

            if (Properties.Settings.Default.nickname is not null)
            {
                Nickname = Properties.Settings.Default.nickname;
            }

            if (Properties.Settings.Default is not null)
            {
                CurrentVersion = Properties.Settings.Default.version;
            }

            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);
        }

        public async Task VersionList()
        {
            await this.windowManager.ShowDialogAsync(this.versionVM);
        }

        public async Task Play()
        {
            clickedPlay = true;

            NotifyOfPropertyChange(() => CanPlay);

            if (Properties.Settings.Default.nickname != Nickname)
            {
                Properties.Settings.Default.nickname = Nickname;
                Properties.Settings.Default.Save();
            }

            await this.dwVersionEndpoint.DownloadVersion(CurrentVersion);

            await launchManager.LaunchGame(CurrentVersion, Nickname, "kz");
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

        public void Changelog()
        {
            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);
        }

        public void ServerList()
        {
            Browser = new Uri("https://betacraft.pl/server.jsp");
            NotifyOfPropertyChange(() => Browser);
        }

        public async Task Language()
        {
            await this.windowManager.ShowDialogAsync(this.languageVM);
        }
    }
}

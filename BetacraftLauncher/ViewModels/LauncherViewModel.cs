using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher.ViewModels
{
    public class LauncherViewModel : Screen, IHandle<SelectVersionEvent>, IHandle<InstanceSettingsEvent>
    {
        private readonly IWindowManager windowManager;
        private readonly VersionViewModel versionVM;
        private readonly LanguageViewModel languageVM;
        private readonly InstanceViewModel instanceVM;
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

        public bool LauncherOpen { get; set; }
        public string InstanceName { get; set; }
        public int GameWidth { get; set; }
        public int GameHeight { get; set; }

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


        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM, LanguageViewModel languageVM, InstanceViewModel instanceVM, IDownloadVersionEndpoint dwVersionEndpoint, ILaunchManager launchManager, IEventAggregator events)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
            this.languageVM = languageVM;
            this.instanceVM = instanceVM;
            this.dwVersionEndpoint = dwVersionEndpoint;
            this.launchManager = launchManager;
            this.events = events;

            events.Subscribe(this);

            LoadSettings();

            Browser = new Uri("https://betacraft.pl/versions/");

            NotifyOfPropertyChange(() => Browser);
        }

        public async Task VersionList()
        {
            await this.windowManager.ShowDialogAsync(this.versionVM);
        }

        public async Task Play()
        {
            if (Nickname.Length > 3)
            {
                clickedPlay = true;

                NotifyOfPropertyChange(() => CanPlay);

                Properties.Settings.Default.nickname = Nickname;
                Properties.Settings.Default.Save();

                await this.dwVersionEndpoint.DownloadVersion(CurrentVersion);

                await launchManager.LaunchGame(CurrentVersion, Nickname, InstanceName, GameWidth.ToString(), GameHeight.ToString());

                if (LauncherOpen == false)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                //dynamic settings = new ExpandoObject();
                //settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                //settings.ResizeMode = ResizeMode.NoResize;
                //settings.Title = "Error";

                //await windowManager.ShowDialogAsync("nah", null, settings);
                MessageBox.Show("Invalid username!");
            }
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
            await windowManager.ShowDialogAsync(this.languageVM);
        }

        public async Task Instance()
        {
            await windowManager.ShowDialogAsync(this.instanceVM);
        }

        public async Task HandleAsync(InstanceSettingsEvent message, CancellationToken cancellationToken)
        {
            //if (message.LauncherOpen == false)
            //{
            //    LauncherOpen = @"javaw";
            //}
            //else
            //{
            //    LauncherOpen = @"java";
            //}

            InstanceName = message.CurrentInstance;
            GameWidth = message.GameWidth;
            GameHeight = message.GameHeight;
            LauncherOpen = message.LauncherOpen;
        }

        private void LoadSettings()
        {
            Nickname = Properties.Settings.Default.nickname;
            CurrentVersion = Properties.Settings.Default.version;
            GameHeight = Properties.Settings.Default.height;
            GameWidth = Properties.Settings.Default.width;
            InstanceName = Properties.Settings.Default.instanceName;
            LauncherOpen = Properties.Settings.Default.keepLauncherOpen;

            //if (Properties.Settings.Default.keepLauncherOpen)
            //{
            //    LauncherOpen = @"java";
            //}
            //else
            //{
            //    LauncherOpen = @"javaw";
            //}
        }
    }
}

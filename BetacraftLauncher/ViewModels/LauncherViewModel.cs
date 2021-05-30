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
        private readonly ILog logger;

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
        public string Arguments { get; set; }

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


        public LauncherViewModel(IWindowManager windowManager, VersionViewModel versionVM, LanguageViewModel languageVM, InstanceViewModel instanceVM, 
            IDownloadVersionEndpoint dwVersionEndpoint, ILaunchManager launchManager, IEventAggregator events, ILog logger)
        {
            this.windowManager = windowManager;
            this.versionVM = versionVM;
            this.languageVM = languageVM;
            this.instanceVM = instanceVM;
            this.dwVersionEndpoint = dwVersionEndpoint;
            this.launchManager = launchManager;
            this.events = events;
            this.logger = logger;

            events.Subscribe(this);

            logger.Info("LauncherViewModel launched");

            LoadSettings();

            BrowserInit();
        }

        private void BrowserInit()
        {
            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);

            logger.Info($"WebBrowser's Uri set to {Browser}");
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

                SaveUsername();

                await this.dwVersionEndpoint.DownloadVersion(CurrentVersion);

                await launchManager.LaunchGame(CurrentVersion, Nickname, InstanceName, GameWidth.ToString(), GameHeight.ToString(), Arguments);

                logger.Info($"Game launched with settings: Version: {CurrentVersion}, Username: {Nickname}, Instance name: {InstanceName}, Width: {GameWidth}, Height: {GameHeight}, Arguments: {Arguments}.");

                if (LauncherOpen == false)
                {
                    logger.Info($"LauncherOpen: {LauncherOpen}. Exiting...");
                    Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("Invalid username!");
                logger.Error(new Exception($"Invalid username: {Nickname}"));
            }
        }

        private void SaveUsername()
        {
            Properties.Settings.Default.nickname = Nickname;
            Properties.Settings.Default.Save();

            logger.Info($"Username {Nickname} saved to settings.");
        }

        public void AuthorsGithub()
        {
            Process.Start(new ProcessStartInfo("https://github.com/KazuOfficial") { 
                UseShellExecute = true
            });

            logger.Info("Author's Github opened");
        }

        public async Task HandleAsync(SelectVersionEvent message, CancellationToken cancellationToken)
        {
            CurrentVersion = message.CurrentVersionMessage;
            NotifyOfPropertyChange(() => CurrentVersion);
            logger.Info($"Received SelectVersionEvent event with content: {message.CurrentVersionMessage}");
        }

        public void Changelog()
        {
            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);
            logger.Info($"WebBrowser's Uri set to {Browser}");
        }

        public void ServerList()
        {
            Browser = new Uri("https://betacraft.pl/server.jsp");
            NotifyOfPropertyChange(() => Browser);
            logger.Info($"WebBrowser's Uri set to {Browser}");
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
            InstanceName = message.CurrentInstance;
            GameWidth = message.GameWidth;
            GameHeight = message.GameHeight;
            LauncherOpen = message.LauncherOpen;
            Arguments = message.Arguments;

            logger.Info($"Received InstanceSettingsEvent with content: InstanceName: {message.CurrentInstance}, GameWidth: {message.GameWidth}, GameHeight: {message.GameHeight}, LauncherOpen: {message.LauncherOpen}, Arguments: {message.Arguments}.");
        }

        private void LoadSettings()
        {
            Nickname = Properties.Settings.Default.nickname;
            CurrentVersion = Properties.Settings.Default.version;
            GameHeight = Properties.Settings.Default.height;
            GameWidth = Properties.Settings.Default.width;
            InstanceName = Properties.Settings.Default.instanceName;
            LauncherOpen = Properties.Settings.Default.keepLauncherOpen;
            Arguments = Properties.Settings.Default.jvmArguments;

            logger.Info("Settings loaded to LauncherViewModel.");
        }
    }
}

using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library.Interfaces;
using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher.ViewModels
{
    public class LauncherViewModel : Screen, IHandle<SelectVersionEvent>, IHandle<InstanceSettingsEvent>
    {
        private readonly IWindowManager _windowManager;
        private readonly VersionViewModel _versionVM;
        private readonly LanguageViewModel _languageVM;
        private readonly InstanceViewModel _instanceVM;
        private readonly IDownloadVersionEndpoint _dwVersionEndpoint;
        private readonly ILaunchManager _launchManager;
        private readonly IEventAggregator _events;
        private readonly ILog _logger;

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
            _windowManager = windowManager;
            _versionVM = versionVM;
            _languageVM = languageVM;
            _instanceVM = instanceVM;
            _dwVersionEndpoint = dwVersionEndpoint;
            _launchManager = launchManager;
            _events = events;
            _logger = logger;

            events.Subscribe(this);

            logger.Info("LauncherViewModel launched");

            LoadSettings();

            BrowserInit();
        }

        private void BrowserInit()
        {
            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);

            _logger.Info($"WebBrowser's Uri set to {Browser}");
        }

        public async Task VersionList()
        {
            await _windowManager.ShowDialogAsync(_versionVM);
        }

        public async Task Play()
        {
            if (Nickname.Length > 3)
            {
                clickedPlay = true;

                NotifyOfPropertyChange(() => CanPlay);

                SaveUsername();

                await _dwVersionEndpoint.DownloadVersion(CurrentVersion);

                await _launchManager.LaunchGame(CurrentVersion, Nickname, InstanceName, GameWidth.ToString(), GameHeight.ToString(), Arguments);

                _logger.Info($"Game launched with settings: Version: {CurrentVersion}, Username: {Nickname}, Instance name: {InstanceName}, Width: {GameWidth}, Height: {GameHeight}, Arguments: {Arguments}.");

                if (LauncherOpen == false)
                {
                    _logger.Info($"LauncherOpen: {LauncherOpen}. Exiting...");
                    Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("Invalid username!");
                _logger.Error(new Exception($"Invalid username: {Nickname}"));
            }
        }

        private void SaveUsername()
        {
            Properties.Settings.Default.nickname = Nickname;
            Properties.Settings.Default.Save();

            _logger.Info($"Username {Nickname} saved to settings.");
        }

        public void AuthorsGithub()
        {
            Process.Start(new ProcessStartInfo("https://github.com/KazuOfficial") { 
                UseShellExecute = true
            });

            _logger.Info("Author's Github opened");
        }

        public async Task HandleAsync(SelectVersionEvent message, CancellationToken cancellationToken)
        {
            CurrentVersion = message.CurrentVersionMessage;
            NotifyOfPropertyChange(() => CurrentVersion);
            _logger.Info($"Received SelectVersionEvent event with content: {message.CurrentVersionMessage}");
        }

        public void Changelog()
        {
            Browser = new Uri("https://betacraft.pl/versions/");
            NotifyOfPropertyChange(() => Browser);
            _logger.Info($"WebBrowser's Uri set to {Browser}");
        }

        public void ServerList()
        {
            Browser = new Uri("https://betacraft.pl/server.jsp");
            NotifyOfPropertyChange(() => Browser);
            _logger.Info($"WebBrowser's Uri set to {Browser}");
        }

        public async Task Language()
        {
            await _windowManager.ShowDialogAsync(_languageVM);
        }

        public async Task Instance()
        {
            await _windowManager.ShowDialogAsync(_instanceVM);
        }

        public async Task HandleAsync(InstanceSettingsEvent message, CancellationToken cancellationToken)
        {
            InstanceName = message.CurrentInstance;
            GameWidth = message.GameWidth;
            GameHeight = message.GameHeight;
            LauncherOpen = message.LauncherOpen;
            Arguments = message.Arguments;

            _logger.Info($"Received InstanceSettingsEvent with content: " +
                $"InstanceName: {message.CurrentInstance}, " +
                $"GameWidth: {message.GameWidth}, " +
                $"GameHeight: {message.GameHeight}, " +
                $"LauncherOpen: {message.LauncherOpen}, " +
                $"Arguments: {message.Arguments}.");
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

            _logger.Info("Settings loaded to LauncherViewModel.");
        }
    }
}

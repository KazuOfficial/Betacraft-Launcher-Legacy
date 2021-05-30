using AutoMapper;
using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library;
using BetacraftLauncher.Library.Models;
using BetacraftLauncher.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher.ViewModels
{
    public class VersionViewModel : Screen
    {
        private readonly IVersionEndpoint versionEndpoint;
        private readonly IWindowManager window;
        private readonly IMapper mapper;
        private readonly IEventAggregator events;
        private readonly ILog logger;

        public VersionViewModel(IVersionEndpoint versionEndpoint, IWindowManager window, IMapper mapper, IEventAggregator events, ILog logger)
        {
            this.versionEndpoint = versionEndpoint;
            this.window = window;
            this.mapper = mapper;
            this.events = events;
            this.logger = logger;
        }

        private BindingList<VersionDisplayModel> _versions;

        public BindingList<VersionDisplayModel> Versions
        {
            get { return _versions; }
            set 
            {
                _versions = value;
                NotifyOfPropertyChange(() => Versions);
            }
        }

        private VersionDisplayModel _selectedVersion;

        public VersionDisplayModel SelectedVersion
        {
            get { return _selectedVersion; }
            set
            {
                _selectedVersion = value;
                NotifyOfPropertyChange(() => SelectedVersion);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            logger.Info("VersionViewModel started.");

            try
            {
                await LoadVersions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                logger.Error(ex);

                await TryCloseAsync();
            }
        }

        private async Task LoadVersions()
        {
            var versionList = await versionEndpoint.GetVersions();
            var versions = mapper.Map<List<VersionDisplayModel>>(versionList);
            Versions = new BindingList<VersionDisplayModel>(versions);

            logger.Info($"VersionDisplayModel binded to Versions BindingList. Versions in list: {Versions.Count}");
        }

        public async Task SelectVersion()
        {
            SaveVersionSettings();

            if (SelectedVersion != null)
            {
                await events.PublishOnUIThreadAsync(new SelectVersionEvent { CurrentVersionMessage = SelectedVersion.Version });

                logger.Info($"Version selected: {SelectedVersion.Version}");
            }

            await TryCloseAsync();
        }

        private void SaveVersionSettings()
        {
            if (SelectedVersion != null)
            {
                Properties.Settings.Default.version = SelectedVersion.Version;
                Properties.Settings.Default.Save();

                logger.Info($"SelectedVersion saved to settings: {SelectedVersion.Version}");
            }
        }
    }
}

using AutoMapper;
using BetacraftLauncher.EventModels;
using BetacraftLauncher.Library.Interfaces;
using BetacraftLauncher.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher.ViewModels
{
    public class VersionViewModel : Screen
    {
        private readonly IVersionEndpoint _versionEndpoint;
        private readonly IWindowManager _window;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _events;
        private readonly ILog _logger;

        public VersionViewModel(IVersionEndpoint versionEndpoint, IWindowManager window, IMapper mapper, IEventAggregator events, ILog logger)
        {
            _versionEndpoint = versionEndpoint;
            _window = window;
            _mapper = mapper;
            _events = events;
            _logger = logger;
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

            _logger.Info("VersionViewModel started.");

            try
            {
                await LoadVersions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _logger.Error(ex);

                await TryCloseAsync();
            }
        }

        private async Task LoadVersions()
        {
            var versionList = await _versionEndpoint.GetVersions();
            var versions = _mapper.Map<List<VersionDisplayModel>>(versionList);
            Versions = new BindingList<VersionDisplayModel>(versions);

            _logger.Info($"VersionDisplayModel binded to Versions BindingList. " +
                $"Versions count: {Versions.Count}");
        }

        public async Task SelectVersion()
        {
            SaveVersionSettings();

            if (SelectedVersion != null)
            {
                await _events.PublishOnUIThreadAsync(new SelectVersionEvent { CurrentVersionMessage = SelectedVersion.Version });

                _logger.Info($"Version selected: {SelectedVersion.Version}");
            }

            await TryCloseAsync();
        }

        private void SaveVersionSettings()
        {
            if (SelectedVersion != null)
            {
                Properties.Settings.Default.version = SelectedVersion.Version;
                Properties.Settings.Default.Save();

                _logger.Info($"SelectedVersion saved to settings: {SelectedVersion.Version}");
            }
        }
    }
}

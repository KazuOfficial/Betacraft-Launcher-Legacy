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

        public VersionViewModel(IVersionEndpoint versionEndpoint, IWindowManager window, IMapper mapper, IEventAggregator events)
        {
            this.versionEndpoint = versionEndpoint;
            this.window = window;
            this.mapper = mapper;
            this.events = events;
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

            try
            {
                await LoadVersions();
            }
            catch (Exception ex)
            {
                //dynamic settings = new ExpandoObject();
                //settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                //settings.ResizeMode = ResizeMode.NoResize;
                //settings.Title = "Error";

                MessageBox.Show(ex.Message);

                await TryCloseAsync();

                //await window.ShowDialogAsync(ex.Message, null, settings);
            }
        }

        private async Task LoadVersions()
        {
           //var versionList = await versionEndpoint.GetVersions();
            //Versions = new BindingList<VersionDisplayModel>(versionList);

            var versionList = await versionEndpoint.GetVersions();
            //Console.WriteLine(versionList);
            var versions = mapper.Map<List<VersionDisplayModel>>(versionList);
            Versions = new BindingList<VersionDisplayModel>(versions);
        }

        public async Task SelectVersion()
        {
            if (SelectedVersion != null)
            {
                Properties.Settings.Default.version = SelectedVersion.Version;
                Properties.Settings.Default.Save();
            }

            //launcherVM.CurrentVersion = SelectedVersion.Version;

            //await events.PublishOnUIThreadAsync(new SelectVersionEvent());
            await events.PublishOnUIThreadAsync(new SelectVersionEvent { CurrentVersionMessage = SelectedVersion.Version });

            await TryCloseAsync();
        }
    }
}

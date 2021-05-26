using AutoMapper;
using BetacraftLauncher.Library;
using BetacraftLauncher.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher.ViewModels
{
    public class LanguageViewModel : Screen
    {
        private readonly ILanguageEndpoint languageEndpoint;
        private readonly IWindowManager window;
        private readonly IMapper mapper;

        public LanguageViewModel(ILanguageEndpoint languageEndpoint, IWindowManager window, IMapper mapper)
        {
            this.languageEndpoint = languageEndpoint;
            this.window = window;
            this.mapper = mapper;
        }

        private BindingList<LanguageDisplayModel> _languages;

        public BindingList<LanguageDisplayModel> Languages
        {
            get { return _languages; }
            set
            { 
                _languages = value;
                NotifyOfPropertyChange(() => Languages);
            }
        }

        private LanguageDisplayModel _selectedLanguage; 

        public LanguageDisplayModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            { 
                _selectedLanguage = value;
                NotifyOfPropertyChange(() => SelectedLanguage);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            try
            {
                await LoadLanguages();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "Error";

                await TryCloseAsync();

                await window.ShowDialogAsync(ex.Message, null, settings);
            }
        }

        private async Task LoadLanguages()
        {
            //var versionList = await versionEndpoint.GetVersions();
            //Versions = new BindingList<VersionDisplayModel>(versionList);

            var languageList = await languageEndpoint.GetLanguages();
            //Console.WriteLine(versionList);
            var languages = mapper.Map<List<LanguageDisplayModel>>(languageList);
            Languages = new BindingList<LanguageDisplayModel>(languages);
        }

        public async Task SelectLanguage()
        {
            if (SelectedLanguage != null)
            {
                Properties.Settings.Default.language = SelectedLanguage.Language;
                Properties.Settings.Default.Save();
            }
            //await events.PublishOnUIThreadAsync(new SelectVersionEvent { CurrentVersionMessage = SelectedVersion.Version });

            await TryCloseAsync();
        }
    }
}

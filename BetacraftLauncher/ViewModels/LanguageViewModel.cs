using AutoMapper;
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
    public class LanguageViewModel : Screen
    {
        private readonly ILanguageEndpoint _languageEndpoint;
        private readonly IWindowManager _window;
        private readonly IMapper _mapper;

        public LanguageViewModel(ILanguageEndpoint languageEndpoint, IWindowManager window, IMapper mapper)
        {
            _languageEndpoint = languageEndpoint;
            _window = window;
            _mapper = mapper;
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
                MessageBox.Show(ex.Message);

                await TryCloseAsync();
            }
        }

        private async Task LoadLanguages()
        {
            var languageList = await _languageEndpoint.GetLanguages();
            var languages = _mapper.Map<List<LanguageDisplayModel>>(languageList);
            Languages = new BindingList<LanguageDisplayModel>(languages);
        }

        public async Task SelectLanguage()
        {
            SaveLanguageSettings();

            await _languageEndpoint.DownloadLanguage(SelectedLanguage.Language);

            await TryCloseAsync();
        }

        private void SaveLanguageSettings()
        {
            if (SelectedLanguage != null)
            {
                Properties.Settings.Default.language = SelectedLanguage.Language;
                Properties.Settings.Default.Save();
            }
        }
    }
}

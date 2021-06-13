using AutoMapper;
using BetacraftLauncher.Helpers;
using BetacraftLauncher.Library;
using BetacraftLauncher.Library.Models;
using BetacraftLauncher.Models;
using BetacraftLauncher.ViewModels;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BetacraftLauncher
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        private IMapper ConfigureAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionModel, VersionDisplayModel>();
                cfg.CreateMap<LanguageModel, LanguageDisplayModel>();
            });

            var output = config.CreateMapper();

            return output;
        }

        private IConfiguration AddConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }


        protected override void Configure()
        {

            _container.Instance(ConfigureAutomapper());

            _container.Instance(_container)
                  .PerRequest<IVersionEndpoint, VersionEndpoint>()
                  .PerRequest<IDownloadVersionEndpoint, DownloadVersionEndpoint>()
                  .PerRequest<IFileInit, FileInit>()
                  .PerRequest<ILaunchManager, LaunchManager>()
                  .PerRequest<ILanguageEndpoint, LanguageEndpoint>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILog, LogHelper>()
                .Singleton<IDiscordRPCManager, DiscordRPCManager>();

            _container.RegisterInstance(typeof(IConfiguration), "IConfiguration", AddConfiguration());

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var fileInit = IoC.Get<IFileInit>();
            fileInit.FileInitialization();
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);

        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

        protected override void BuildUp(object instance) => _container.BuildUp(instance);

        protected override void OnExit(object sender, EventArgs e)
        {
            var discordRPC = IoC.Get<IDiscordRPCManager>();
            discordRPC.Deinitialize();
        }
    }
}
using AutoMapper;
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
            });

            var output = config.CreateMapper();

            return output;
        }

        //        private IConfiguration AddConfiguration()
        //        {
        //            IConfigurationBuilder builder = new ConfigurationBuilder()
        //                .SetBasePath(Directory.GetCurrentDirectory())
        //                .AddJsonFile("appsettings.json");

        //#if DEBUG
        //            builder.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
        //#else
        //            builder.AddJsonFile("appsettings.production.json", optional: true, reloadOnChange: true);
        //#endif

        //            return builder.Build();
        //        }

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
                  .PerRequest<IDownloadVersionEndpoint, DownloadVersionEndpoint>();
            //    .PerRequest<IProductEndpoint, ProductEndpoint>()
            //    .PerRequest<IUserEndpoint, UserEndpoint>()
            //    .PerRequest<ISaleEndpoint, SaleEndpoint>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();
                //.Singleton<ILoggedInUserModel, LoggedInUserModel>()
                //.Singleton<IAPIHelper, APIHelper>();

            _container.RegisterInstance(typeof(IConfiguration), "IConfiguration", AddConfiguration());

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        //protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<ShellViewModel>();
        protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<ShellViewModel>();

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);

        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}

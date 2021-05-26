using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class FileInit : IFileInit
    {
        private readonly IConfiguration config;

        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy";
        public FileInit(IConfiguration config)
        {
            this.config = config;
        }

        public async Task FileInitialization()
        {
            if (!Directory.Exists(launcherPath))
            {
                Directory.CreateDirectory(launcherPath + @"\versions\jsons\");
                Directory.CreateDirectory(launcherPath + @"\launcher\lang\");
                Directory.CreateDirectory(launcherPath + @"\bin\natives\");
            }

            if (!File.Exists(launcherPath + @"\launcher\betacraft_wrapper.jar"))
            {
                using (WebClient webClient = new())
                {
                    await webClient.DownloadFileTaskAsync(new Uri(config.GetValue<string>("wrapperURL")), launcherPath + $@"\launcher\betacraft_wrapper.jar");
                }
            }
        }
    }
}

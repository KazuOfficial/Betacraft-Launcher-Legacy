using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class DownloadVersionEndpoint : IDownloadVersionEndpoint
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\betacraftlegacy\";

        private readonly IConfiguration config;

        public DownloadVersionEndpoint(IConfiguration config)
        {
            this.config = config;
        }

        public async Task DownloadVersion(string versionName)
        {
            using (WebClient webClient = new())
            {
                await webClient.DownloadFileTaskAsync(new Uri(config.GetValue<string>("versionJsonList") + $"{versionName}.info"), launcherPath + $@"\versions\jsons\{versionName}.info");
            }
        }
    }
}

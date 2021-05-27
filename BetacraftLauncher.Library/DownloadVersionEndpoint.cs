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
    public class DownloadVersionEndpoint : IDownloadVersionEndpoint
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy";

        public async Task DownloadVersion(string versionName)
        {
            await DownloadVersionInfo(versionName);

            var versionURL = File.ReadAllLines(launcherPath + $@"\versions\jsons\{versionName}.info").Where(line => line.Contains("url:")).First().Replace("url:", "");

            using (WebClient webClient = new())
            {
                await webClient.DownloadFileTaskAsync(versionURL, $@"{launcherPath}\versions\{versionName}.jar");
            }
        }

        private async Task DownloadVersionInfo(string versionName)
        {
            using (WebClient webClient = new())
            {
                await webClient.DownloadFileTaskAsync("https://files.betacraft.pl/launcher/assets/jsons/" + $"{versionName}.info", launcherPath + $@"\versions\jsons\{versionName}.info");
            }
        }
    }
}

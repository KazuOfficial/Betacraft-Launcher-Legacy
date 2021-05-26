using BetacraftLauncher.Library.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class VersionEndpoint : IVersionEndpoint
    {
        private string versionPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\launcher\versions.txt";

        public async Task<List<VersionModel>> GetVersions()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string versionList = await webClient.DownloadStringTaskAsync("https://files.betacraft.pl/launcher/assets/version_list.txt");

                    return await VersionListFileManager(versionList);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        private async Task<List<VersionModel>> VersionListFileManager(string versionList)
        {
            List<VersionModel> result = new();

            await File.WriteAllTextAsync(versionPath, versionList);

            var lines = File.ReadLines(versionPath);

            foreach (var line in lines)
            {
                string[] x = line.Split("`");
                result.Add(new VersionModel { Version = x[0] });
            }

            File.Delete(versionPath);

            return result;
        }
    }
}

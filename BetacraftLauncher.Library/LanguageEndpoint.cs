using BetacraftLauncher.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class LanguageEndpoint : ILanguageEndpoint
    {
        private string languagePath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\launcher\lang\lang.txt";
        public async Task<List<LanguageModel>> GetLanguages()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string versionList = await webClient.DownloadStringTaskAsync("https://betacraft.pl/lang/1.09_11/");

                    return await LanguageFileManager(versionList);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        private async Task<List<LanguageModel>> LanguageFileManager(string languageList)
        {
            List<LanguageModel> result = new();

            await File.WriteAllTextAsync(languagePath, languageList);

            var lines = File.ReadLines(languagePath);

            foreach (var line in lines)
            {
                string[] x = line.Split("`");
                result.Add(new LanguageModel { Language = x[0] });
            }

            File.Delete(languagePath);

            return result;
        }
    }
}

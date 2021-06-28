using BetacraftLauncher.Library.Interfaces;
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
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\";

        public void FileInitialization()
        {
            if (!Directory.Exists(launcherPath))
            {
                Directory.CreateDirectory(launcherPath + @"\versions\jsons\");
                Directory.CreateDirectory(launcherPath + @"\launcher\");
                Directory.CreateDirectory(launcherPath + @"\bin\natives\");
            }

            if (!File.Exists(launcherPath + @"\launcher\betacraft_wrapper.jar"))
            {
                using (WebClient webClient = new())
                {
                    webClient.DownloadFile("https://files.betacraft.pl/improvedjsons/bcwrapper-1.0.1-pre3.jar", launcherPath + $@"\launcher\betacraft_wrapper.jar");
                }
            }

            //if (!File.Exists(launcherPath + @"\launcher\lang\English.txt"))
            //{
            //    using (WebClient webClient = new())
            //    {
            //        await webClient.DownloadFileTaskAsync("https://betacraft.pl/lang/1.09_11/English.txt", launcherPath + $@"\launcher\lang\English.txt");
            //    }
            //}
        }
    }
}

using BetacraftLauncher.Library.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace BetacraftLauncher.Library
{
    public class FileInit : IFileInit
    {
        private static string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\";

        private string[] launcherDirectories = new string[] 
        {
            launcherPath + @"\versions\jsons\",
            launcherPath + @"\launcher\",
            launcherPath + @"\bin\natives\"
        };

        public void FileInitialization()
        {
            if (!Directory.Exists(launcherPath))
            {
                for (int i = 0; i < launcherDirectories.Count(); i++)
                {
                    Directory.CreateDirectory(launcherDirectories[i]);
                }
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

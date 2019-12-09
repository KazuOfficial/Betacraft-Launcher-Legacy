using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.IO;

namespace Betacraft_Launcher
{
    class Launcher
    {
        static WebClient client = new WebClient();
        static Process proc = new Process();
        static Password pww = new Password();

        static string appData = Environment.GetEnvironmentVariable("APPDATA");
        static string DES = appData + @"\betacraft\";
        static string createText;
    
        public static void LaunchGame(string username)
        {
            Process.Start(@"javaw", @"-Xms1024m -Xmx1024m -cp """ + appData + @"\betacraft\betacraft\bin\*"" -Djava.library.path=""" + appData + @"\betacraft\betacraft\bin\natives"" net.minecraft.client.Minecraft " + username);
            Environment.Exit(0);
        }

        public static void LaunchWebsite(string url)
        {
            Process.Start(url);
        }

        public static void Download(string labele, string download, string path, string write)
        {
            createText = labele + Environment.NewLine;
            MessageBox.Show("Dostępna jest aktualizacja. Wciśnij OK aby ją pobrać. (To może zająć chwilę. Program się wyłączy by nie przeszkadzać użytkownikowi w korzystaniu z komputera, a potem włączy gdy aktualizacja dobiegnie końca.)");
            client.DownloadFile("https://betacraft.pl/client/unrar.exe", appData + @"\betacraft\unrar.exe");
            client.DownloadFile(download, path);
            File.WriteAllText(write, createText);

            if (Directory.Exists(appData + @"\betacraft\betacraft"))
            {
                Directory.Delete(appData + @"\betacraft\betacraft", true);
            }
            Unrar(appData + @"\betacraft\unrar.exe", path);
        }

        public static void Fix()
        {
            if (!Directory.Exists(appData + @"\.minecraft\"))
            {
                Directory.CreateDirectory(appData + @"\.minecraft\");
            }
            if (!Directory.Exists(appData + @"\.minecraft\mods"))
            {
                Directory.CreateDirectory(appData + @"\.minecraft\mods");
            }
            if (!Directory.Exists(appData + @"\.minecraft\mods\zombe"))
            {
                Directory.CreateDirectory(appData + @"\.minecraft\mods\zombe");
            }
            if (Directory.Exists(appData + @"\.minecraft\mods\zombe"))
            {
                Directory.Delete(appData + @"\.minecraft\mods\zombe", true);
            }
            if (!Directory.Exists(appData + @"\.minecraft\mods\zombe"))
            {
                Directory.CreateDirectory(appData + @"\.minecraft\mods\zombe");
            }
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/config.txt", appData + @"\.minecraft\mods\zombe\config.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/fuel.txt", appData + @"\.minecraft\mods\zombe\fuel.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/log.txt", appData + @"\.minecraft\mods\zombe\log.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/names.txt", appData + @"\.minecraft\mods\zombe\names.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/recipes-clean.txt", appData + @"\.minecraft\mods\zombe\recipes-clean.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/recipes-example.txt", appData + @"\.minecraft\mods\zombe\recipes-example.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/recipes-mp.txt", appData + @"\.minecraft\mods\zombe\recipes-mp.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/recipes-vanilla.txt", appData + @"\.minecraft\mods\zombe\recipes-vanilla.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/recipes.txt", appData + @"\.minecraft\mods\zombe\recipes.txt");
            client.DownloadFile("https://betacraft.pl/client/optionalupdate/smelting.txt", appData + @"\.minecraft\mods\zombe\smelting.txt");
        }
        
        public static void Unrar(string filepath, string SRC)
        {
                proc.StartInfo.FileName = filepath;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.EnableRaisingEvents = true;

                proc.StartInfo.Arguments = String.Format("x -p{0} {1} {2}", "duh", SRC, DES);
                proc.Start();
            }

        public static void Blur()
        {
            if(pww.window.IsVisible == true)
            {
                //MainWindow.VisibilityProperty = Visibility.Hidden;
            }
        }

        }

    }
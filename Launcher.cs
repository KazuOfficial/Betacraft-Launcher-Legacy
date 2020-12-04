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
            //MessageBox.Show("Update available! Click OK to download the update.");
            client.DownloadFile("https://betacraft.pl/client/unrar.exe", appData + @"\betacraft\unrar.exe");
            client.DownloadFile(download, path);
            File.WriteAllText(write, createText);

            if (Directory.Exists(appData + @"\betacraft\betacraft"))
            {
                Directory.Delete(appData + @"\betacraft\betacraft", true);
            }
            Unrar(appData + @"\betacraft\unrar.exe", path);
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

        }

    }
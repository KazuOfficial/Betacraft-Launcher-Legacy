using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class LaunchManager : ILaunchManager
    {
        private static string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy";
        private string nativesPath { get; } = $@"{launcherPath}\bin\natives";

        public async Task LaunchGame(string versionName, string userName, string frameName)
        {
            await DownloadNatives();

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = $@"java -cp {launcherPath}\launcher\betacraft_wrapper.jar;{launcherPath}\versions\{versionName}.jar;{launcherPath}\bin\lwjgl.jar;{launcherPath}\bin\lwjgl_util.jar;{launcherPath}\bin\jinput.jar pl.moresteck.BCWrapper username={userName} sessionid= gameDir={launcherPath} versionName={versionName} frameName={frameName} width=854 height=480 assetsDir= nativesDir={nativesPath}"
            };

            Process.Start(processStartInfo);
        }

        private async Task DownloadNatives()
        {
            if (!File.Exists($@"{nativesPath}\jinput-raw_64.dll"))
            {
                using (WebClient webClient = new())
                {
                    await webClient.DownloadFileTaskAsync("https://files.betacraft.pl/launcher/assets/natives-windows.zip", $@"{nativesPath}\natives-windows.zip");
                }

                ExtractZip($@"{nativesPath}\natives-windows.zip", nativesPath);
            }

            if (!File.Exists($@"{launcherPath}\bin\lwjgl.jar"))
            {
                using (WebClient webClient = new())
                {
                    await webClient.DownloadFileTaskAsync("https://files.betacraft.pl/launcher/assets/libs.zip", $@"{launcherPath}\bin\libs.zip");
                }

                ExtractZip($@"{launcherPath}\bin\libs.zip", $@"{launcherPath}\bin");
            }

        }

        private void ExtractZip(string zipDir, string destination)
        {
            ZipFile.ExtractToDirectory(zipDir, destination);

            File.Delete(zipDir);
        }
    }
}

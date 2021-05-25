using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class LaunchManager : ILaunchManager
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\";

        public void LaunchGame(string version, string userName, string versionName, string frameName)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                WorkingDirectory = $@"{launcherPath}launcher\",
                CreateNoWindow = true,
                Arguments = $@"java -cp betacraft_wrapper.jar;{version}.jar;lwjgl.jar;lwjgl_util.jar;jinput.jar pl.moresteck.BCWrapper username={userName} sessionid= gameDir={launcherPath} versionName={versionName} frameName={frameName} width=854 height=480 assetsDir={launcherPath} nativesDir={launcherPath}\bin\natives"
            };

            Process.Start(processStartInfo);
        }
    }
}

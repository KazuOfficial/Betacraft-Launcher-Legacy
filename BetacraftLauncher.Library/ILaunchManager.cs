using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public interface ILaunchManager
    {
        Task LaunchGame(string versionName, string userName, string frameName, string windowWidth, string windowHeight, string arguments);
    }
}
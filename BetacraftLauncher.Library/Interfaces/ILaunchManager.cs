using System.Threading.Tasks;

namespace BetacraftLauncher.Library.Interfaces
{
    public interface ILaunchManager
    {
        Task LaunchGame(string versionName, string userName, string frameName, string windowWidth, string windowHeight, string arguments);
    }
}
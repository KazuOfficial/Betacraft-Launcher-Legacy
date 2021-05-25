using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public interface IDownloadVersionEndpoint
    {
        Task DownloadVersion(string versionName);
    }
}
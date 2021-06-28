using System.Threading.Tasks;

namespace BetacraftLauncher.Library.Interfaces
{
    public interface IDownloadVersionEndpoint
    {
        Task DownloadVersion(string versionName);
    }
}
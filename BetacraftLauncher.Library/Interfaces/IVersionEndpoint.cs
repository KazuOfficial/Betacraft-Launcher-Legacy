using BetacraftLauncher.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library.Interfaces
{
    public interface IVersionEndpoint
    {
        Task<List<VersionModel>> GetVersions();
    }
}
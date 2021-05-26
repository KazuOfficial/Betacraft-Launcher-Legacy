using BetacraftLauncher.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public interface ILanguageEndpoint
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}
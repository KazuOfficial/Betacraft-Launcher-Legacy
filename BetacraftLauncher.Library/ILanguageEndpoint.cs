using BetacraftLauncher.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public interface ILanguageEndpoint
    {
        Task DownloadLanguage(string languageName);
        Task<List<LanguageModel>> GetLanguages();
    }
}
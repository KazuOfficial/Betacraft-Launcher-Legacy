using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BetacraftLauncher.Tests
{
    public class VersionEndpointTests
    {
        [Fact]
        public async void AddToList_ShouldAdd()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string result = await webClient.DownloadStringTaskAsync(new Uri("https://files.betacraft.pl/launcher/assets/version_list.txt"));

                    List<string> s = new();

                    foreach (var line in result)
                    {
                        s.Add(result);
                    }

                    Assert.Equal("x", result);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}

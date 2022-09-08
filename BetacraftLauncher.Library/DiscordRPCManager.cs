using BetacraftLauncher.Library.Interfaces;
using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Extensions.Configuration;

namespace BetacraftLauncher.Library
{
    public class DiscordRPCManager : IDiscordRPCManager
    {
        private readonly IConfiguration _config;
        private DiscordRpcClient client;

        public DiscordRPCManager(IConfiguration config)
        {
            _config = config;
        }

        public void Initialize()
        {
            client = new DiscordRpcClient(_config.GetValue<string>("DiscordClientID"));

            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            client.Initialize();

            client.SetPresence(new RichPresence());
        }

        public void Deinitialize()
        {
            client.Dispose();
        }
    }
}
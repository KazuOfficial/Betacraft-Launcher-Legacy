using BetacraftLauncher.Library.Interfaces;
using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetacraftLauncher.Library
{
    public class DiscordRPCManager : IDiscordRPCManager
    {
        private readonly IConfiguration config;
        private DiscordRpcClient client;

        public DiscordRPCManager(IConfiguration config)
        {
            this.config = config;
        }

        public void Initialize()
        {
            client = new DiscordRpcClient(config.GetValue<string>("DiscordClientID"));

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
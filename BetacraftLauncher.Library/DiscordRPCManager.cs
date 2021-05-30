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
            client = new DiscordRpcClient(config.GetValue<string>("discordClientKey"));

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) =>
            {
                //Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                //Console.WriteLine("Received Update! {0}", e.Presence);
            };

            //Connect to the RPC
            client.Initialize();

            //Set the rich presence
            //Call this as many times as you want and anywhere in your code.
            client.SetPresence(new RichPresence()
            {
                //Details = "Betacraft Launcher Legacy",
                //State = "csharp example",
                //Assets = new Assets()
                //{
                //    LargeImageKey = "image_large",
                //    LargeImageText = "Lachee's Discord IPC Library",
                //    SmallImageKey = "image_small"
                //}
            });
        }

        public void Deinitialize()
        {
            client = new DiscordRpcClient(config.GetValue<string>("discordClientKey"));

            client.Dispose();
        }
    }
}

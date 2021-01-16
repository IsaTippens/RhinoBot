using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Addons.Interactive;

using RhinoBot.Core.Services;
using RhinoBot.Core.Utilities;

using Microsoft.Extensions.DependencyInjection;

namespace RhinoBot
{
    public class RhinoBot
    {
        private readonly string _tokenID;
        private IServiceProvider provider;

        public RhinoBot()
        {
            _tokenID = Environment.GetEnvironmentVariable("TOKEN_ID");
        }

        private async Task Initialise()
        {
            var client = new DiscordSocketClient();
            provider = new ServiceCollection()
            .AddSingleton(client)
            .AddSingleton(new CommandService())
            .AddSingleton(new InteractiveService(client))
            .AddSingleton<CommandHandler>()
            .AddSingleton<JsonService>()
            .BuildServiceProvider();
        }

        public async Task Run()
        {
            await Initialise();
            var _client = provider.GetService<DiscordSocketClient>();
            var _service = provider.GetService<CommandService>();
            var _handler = provider.GetService<CommandHandler>();

            LogService logger = new LogService(_client, _service);

            await _client.LoginAsync(TokenType.Bot, _tokenID);
            await _client.StartAsync();
            await _client.SetGameAsync("Rhino Poacher Hunter");

            await _handler.InitialiseAsync();

            await Task.Delay(-1);
        }
    }
}


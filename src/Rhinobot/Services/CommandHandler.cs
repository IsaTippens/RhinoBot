using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace RhinoBot.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        public CommandHandler(IServiceProvider services, DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }

        public async Task InitialiseAsync()
        {
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: _services);
            _client.MessageUpdated += MessageUpdated;
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            
            var msg = messageParam.Content.ToLower();

            if (msg.Contains("i'm down") || msg.Contains("im down") || msg.Contains("i am down"))
            {
                await messageParam.Channel.SendMessageAsync("syndrome");
                return;
            }

            int argPos = 0;
            if (ProcessMessage(ref argPos, message) == -1)
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }

        private int ProcessMessage(ref int argPos, SocketUserMessage message)
        {
            if (message.Author.IsBot)
            {
                return -1;
            }

            var content = message.Content.ToLower();
            var prefixes = new string[] {
                $"<@!{_client.CurrentUser.Id}>",
                "yo rhinobot",
                "yo rhino bot",
                "rhinobot",
                "rhino bot",
                "yo rhinoboi",
                "yo rhino boi",
                "rhinoboi",
                "rhino boi",
            };

            bool result = false;
            foreach (string prefix in prefixes)
            {

                if (content.StartsWith(prefix))
                {
                    result = true;
                    argPos = prefix.Length + (prefix == "!" ? 0 : 1);
                    break;
                }
                else if (content.Contains(prefix))
                {
                    result = true;
                    argPos = 0;
                    break;
                }
            }
            if (!result)
            {
                return -1;
            }

            return argPos;
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }

    }
}
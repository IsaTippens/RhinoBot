using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using RhinoBot.Core.Utilities;

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
            if (message == null || message.Author.IsBot) return;


            var msg = messageParam.Content.ToLower();
            if (true)
            {

                if (msg.Contains("i'm down") || msg.Contains("im down") || msg.Contains("i am down"))
                {
                    await messageParam.Channel.SendMessageAsync("syndrome");
                    return;
                }
                if (msg.Contains("nae nae"))
                {
                    await messageParam.Channel.SendMessageAsync("They got nae nae'd");
                    return;
                }
                if (msg.Contains("am i gay") || msg.Contains("i am gay") || msg.Contains("im gay") || msg.Contains("i'm gay"))
                {
                    await messageParam.Channel.SendMessageAsync("Yes you are!");
                    return;
                }
                if (msg.StartsWith("liam") || msg.StartsWith("lem") || msg.StartsWith("lemm") || msg.StartsWith("liamm"))
                {
                    if (Randomiser.RNG.Next(0, 100) > 60)
                        await messageParam.Channel.SendMessageAsync("LIAMM!!!");
                    return;
                }

                if (msg.Contains("banned") || msg.Contains("ban"))
                {
                    await messageParam.Channel.SendMessageAsync("Hammer Time");
                    return;
                }
                if (msg.Contains("doggo") || msg.Contains("pupper"))
                {
                    await messageParam.Channel.SendMessageAsync("OwO doggo");
                    return;
                }
                
                if (msg.Contains("gme") || msg.Contains("$gme"))
                {
                    await messageParam.Channel.SendMessageAsync("HOLD THE LINE!!🚀");
                    return;
                }

                if (Regex.IsMatch(msg, @"\bim\b"))
                {
                    Regex rx = new Regex(@"\bim\b");
                    foreach (Match m in rx.Matches(msg))
                    {
                        int i = m.Index;
                        var result = msg.Substring(i + 3);
                        await messageParam.Channel.SendMessageAsync("Hi " + result);
                        return;
                    }
                }

                if (Regex.IsMatch(msg, @"\bi'm\b"))
                {
                    Regex rx = new Regex(@"\bi'm\b");
                    foreach (Match m in rx.Matches(msg))
                    {
                        int i = m.Index;
                        var result = msg.Substring(i + 4);
                        await messageParam.Channel.SendMessageAsync("Hi " + result);
                        return;
                    }
                }

               if (Regex.IsMatch(msg, @"\bi am\b"))
                {
                    Regex rx = new Regex(@"\bi am\b");
                    foreach (Match m in rx.Matches(msg))
                    {
                        int i = m.Index;
                        var result = msg.Substring(i + 5);
                        await messageParam.Channel.SendMessageAsync("Hi " + result);
                        return;
                    }
                }
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

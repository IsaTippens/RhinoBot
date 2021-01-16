using System;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using RhinoBot.Core.Utilities;

namespace RhinoBot.Core.Services
{
    public class LogService
    {
        Logger _log;
         public LogService(DiscordSocketClient client, CommandService commandService)
        {
            client.Log += Log;
            client.Connected += Connected;

            commandService.Log += Log;

            _log = new Logger();
        }

        private async Task Log(LogMessage message)
        {
            await _log.Log(message);
        }

        private async Task Connected()
        {
            await _log.Log("Bot connected!");
        }
    }
}
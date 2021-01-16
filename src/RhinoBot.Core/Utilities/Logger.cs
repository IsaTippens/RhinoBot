using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.Logging;

namespace RhinoBot.Core.Utilities
{
    public class Logger
    {
        private Dictionary<LogSeverity, Color> LogColor = new Dictionary<LogSeverity, Color>()
        {
            {LogSeverity.Critical, Color.Red},
            {LogSeverity.Error, Color.Red},
            {LogSeverity.Warning, Color.Orange},
            {LogSeverity.Info, Color.Blue},
            {LogSeverity.Debug, Color.Green},
            {LogSeverity.Verbose, Color.Gold},
        };
        public Logger()
        {

        }

        public Task Log(string message)
        {
            return Log(LogSeverity.Info, message);
        }

        public Task Log(LogSeverity severity, string message)
        {
            return Log("System", severity, message);
        }

        public Task Log(LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                return Log("Command", message.Severity, $"{cmdException.Command.Aliases[0]}"
                    + $" failed to execute in {cmdException.Context.Channel}.\n{cmdException}");

            }
            else
            {
                return Log("General", message.Severity, message.Message);
            }
        }

        private Task Log(string sender, LogSeverity severity, string message)
        {
            Colorful.Console.Write($"[{sender}/", Color.LighterGrey);
            Colorful.Console.Write(severity, LogColor[severity]);
            Colorful.Console.Write($"] {message}\n", Color.LighterGrey);
            return Task.CompletedTask;
        }


    }
}

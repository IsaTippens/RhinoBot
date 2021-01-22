using System.Threading.Tasks;
using System;
using System.Diagnostics;

using Discord;
using Discord.Commands;
using Discord.Addons.Interactive;

using RhinoBot.Core.Utilities;

[RequireOwner()]
public class OwnerModule : InteractiveBase
{
    [Command("Update")]
    public async Task UpdateAsync()
    {
        await ReplyAsync("Updating...");
        var command = "sh";
        var myBatchFile = "/home/isatippens2/rhinohome/RhinoBot/stubUpdate";
        var argss = $"{myBatchFile}";

        var processInfo = new ProcessStartInfo();
        processInfo.UseShellExecute = false;
        processInfo.FileName = command;   // 'sh' for bash 
        processInfo.Arguments = argss;
        Process.Start(processInfo);
        Process.GetCurrentProcess().Kill();
    }

    [Command("ShutDown", RunMode = RunMode.Async)]
    [Alias("shut down")]
    public async Task ShutDownAsync()
    {   
        string keys ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string phrase = "";
        for (int i = 0; i < 5; i++)
        {
            phrase += keys[Randomiser.RNG.Next(0, keys.Length)];
        }
        await ReplyAsync($"Reply with {phrase} to confirm shut down");

        var reply = await NextMessageAsync(timeout: TimeSpan.FromMinutes(1.0));
        if (reply == null || reply.Content != phrase) {
            await  reply.AddReactionAsync(new Emoji("❌"));
            return;
        }
        await reply.AddReactionAsync(new Emoji("✅"));
        await ReplyAsync("Shutting down...");
        Process.GetCurrentProcess().Kill();
    }

    [Command("Pokey")]
    public async Task PokeyAsync()
    {
        await ReplyAsync("PokeyMahn!");
    }

    [Command("Oof")]
    public async Task OofAsync()
    {
        await ReplyAsync("The biggest rhino gets the largest oof");
    }
}

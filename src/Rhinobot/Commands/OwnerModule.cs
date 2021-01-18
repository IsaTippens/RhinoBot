using System.Threading.Tasks;
using System.Diagnostics;

using Discord;
using Discord.Commands;

[RequireOwner()]
public class OwnerModule : ModuleBase<SocketCommandContext>
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

    [Command("ShutDown")]
    [Alias("shut down")]
    public async Task ShutDownAsync()
    {
        await ReplyAsync("Shutting Down");
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

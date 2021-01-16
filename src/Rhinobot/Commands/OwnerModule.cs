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

    [Command("Oof")]
    public async Task OofAsync()
    {
        await ReplyAsync("Oof looks like i got updated lolz xd :D");
    }
}
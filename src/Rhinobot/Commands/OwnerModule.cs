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
        var process = Process.Start(processInfo);
    }

    [Command("Oof")]
    public async Task OofAsync()
    {
        await ReplyAsync("Hey imma oof real quick lol! :D");
    }
}
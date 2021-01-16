using System.Threading.Tasks;
using System.Diagnostics.Process;

using Discord;
using Discord.Commands;

[RequireOwner()]
public class OwnerModule : ModuleBase<SocketCommandContext>
{
    [Command("Update")]
    public async Task UpdateAsync([Remainder] string message = null)
    {
        await ReplyAsync("Updating...");
        var p = Process.Start("/home/isatippens2/rhinohome/RhinoBot/update");
    }
}
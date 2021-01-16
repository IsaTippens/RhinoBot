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
        var p = Process.Start("/home/isatippens2/rhinohome/RhinoBot/update");
    }

    [Command("Oof")]
    public async Task UpdateAsync()
    {
        await ReplyAsync("Oof the update is here! :D");
    }
}
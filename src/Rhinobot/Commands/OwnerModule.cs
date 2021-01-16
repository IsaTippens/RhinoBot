using System.Threading.Tasks;

using Discord;
using Discord.Commands;

[RequireOwner()]
public class OwnerModule : ModuleBase<SocketCommandContext>
{
    [Command("Update")]
    public async Task UpdateAsync([Remainder] string message = null)
    {
        await ReplyAsync("UwU");
    }
}
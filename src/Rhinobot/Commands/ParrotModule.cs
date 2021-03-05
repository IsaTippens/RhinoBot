using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
public class ParrotModule : ModuleBase<SocketCommandContext>
{
    [Command("say")]
    [Summary("Echoes a message.")]
    public async Task SayAsync([Remainder][Summary("The text to echo")] string echo)
    {
        var msg = Context.Message;
        //await msg.AddReactionAsync(new Emoji("💛"));
        await ReplyAsync(echo);
    }

    [Command("tell")]
    [Summary("Echoes a message.")]
    public async Task SayAsync(SocketUser user = null, [Remainder] string message = null)
    {
        var msg = Context.Message;
        if (user == null) 
        {
            await ReplyAsync("Tell who?");
            return;
        }
        if (message == null) 
        {
            await ReplyAsync("What should I tell them?");
            return;
        }
        //await msg.AddReactionAsync(new Emoji("💛"));
        await ReplyAsync($"@{user.Username}#{user.Discriminator} {message}");
    }
}

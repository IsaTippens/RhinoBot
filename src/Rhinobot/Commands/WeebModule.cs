using System.Threading.Tasks;

using Discord;
using Discord.Commands;
public class WeebModule : ModuleBase<SocketCommandContext>
{
    [Command("uwu")]
    public async Task UwuAsync([Remainder] string message = null)
    {
        var msg = Context.Message;
        var userId = Context.User.Id;
        if (userId == 258259111437795328)
        {
            await ReplyAsync("Rourke no");
            return;
        }
        if (userId == 244408058011189259)
        {
            await ReplyAsync("No can do sir");
            return;
        }

        if (false) //|| userId == 356423260742746127 || userId == 265935549745856533)
        {
            var I = char.ConvertFromUtf32(0xE0049);
            var L = char.ConvertFromUtf32(0xE004C);
            var O = char.ConvertFromUtf32(0xE004F);
            var S = char.ConvertFromUtf32(0xE0053);
            var T = char.ConvertFromUtf32(0xE0054);
            await msg.AddReactionAsync(new Emoji(I));
            await msg.AddReactionAsync(new Emoji(L));
            await msg.AddReactionAsync(new Emoji(O));
            await msg.AddReactionAsync(new Emoji(S));
            await msg.AddReactionAsync(new Emoji(T));
        }
        await ReplyAsync("UwU");
    }

    [Command("owo")]
    public async Task OwoAsync([Remainder] string message = null)
    {
        var msg = Context.Message;
        var userId = Context.User.Id;
        if (userId == 258259111437795328)
        {
            await ReplyAsync("Rourke no");
            return;
        }
        if (userId == 244408058011189259)
        {
            await ReplyAsync("I refuse");
            return;
        }
        
        await ReplyAsync("OwO");
    }
}
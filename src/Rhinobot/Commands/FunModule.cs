using System;
using System.Threading;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;

using RhinoBot.Core.Utilities;


public class FunModule : InteractiveBase
{
    [Group("Throw")]
    public class ThrowGroup : InteractiveBase
    {

    }
    [Group("flip")]
    public class FlipGroup : InteractiveBase
    {
        [Command("coin")]
        public async Task FlipCoin()
        {
            int comment = Randomiser.RNG.Next(0, 10);
            string[] comments = new[] {
            "Blows into hands",
            "Pulls out lucky charms",
            "Quickly checks horoscope",
            "Pulls out Uno Reverse to bad luck",
            "Breaks a leg",
            "Turns a horse shoe the right way around",
            "Borrowing Liam's luck",
            "Prepares Attempt #3",
            "Pulls out dice rolling arm",
            "Yeeeet"
            };
            if (Randomiser.RNG.Next(0, 1000) > 850)
            {
                await ReplyAsync($"* {comments[comment]} *");
            }
            var user = Context.User;
            string result = Randomiser.RNG.Next(0, 2) == 0 ? "Heads" : "Tails";
            await ReplyAsync($"<@{user.Id}> you landed {result}");
        }

        [Command("table")]
        public async Task FlipTable()
        {
            await ReplyAsync("(ﾉ≧∇≦)ﾉ ﾐ ┸━┸");
        }
    }



    [Command("Roll")]
    public async Task RollDie(int sides)
    {
        if (sides < 2)
        {
            await ReplyAsync("That would rip open a black hole");
            return;
        }
        if (sides == 2)
        {
            await ReplyAsync("Would you rather like to ```flip coin```");
            return;
        }

        int comment = Randomiser.RNG.Next(0, 10);
        string[] comments = new[] {
            "Blows into hands",
            "Pulls out lucky charms",
            "Quickly checks horoscope",
            "Pulls out Uno Reverse to bad luck",
            "Breaks a leg",
            "Turns a horse shoe the right way around",
            "Borrowing Liam's luck",
            "Prepares Attempt #3",
            "Pulls out dice rolling arm",
            "Yeeeet"
        };
        await ReplyAsync($"Preparing {sides} sided die");
        if (Randomiser.RNG.Next(0, 1000) > 850)
        {
            await ReplyAsync($"* {comments[comment]} *");
        }
        var user = Context.User;
        await ReplyAsync($"<@{user.Id}> you rolled a {Randomiser.RNG.Next(1, sides + 1)!}");

    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

using RhinoBot.Core.Utilities;

namespace RhinoBot
{
    public class SmallTalkModule : ModuleBase<SocketCommandContext>
    {
        [Command("hey")]
        [Alias("hello", "hi", "hoi")]
        public async Task Greeting([Remainder] string message = null)
        {
            var user = Context.User;
            var emoji = "😄";

            await ReplyAsync($"Hi {user.Username}! {emoji}");
        }
        
        
        [Command("how you")]
        [Alias("how are you")]
        public async Task Checkup([Remainder] string message = null)
        {
            var msg = message == null ? "" : message.ToLower();
            //Spaghetti conditions
            if (msg.Contains("going") || msg.Contains("that") || (msg.Contains("this") && !msg.Contains("day")))
            {
                return;
            }
            var user = Context.User;
            var emojiGood = "😄";
            var emojiBad = "😵";
            int chance = Randomiser.RNG.Next(10000);
            if (chance == 777)
            {
                await ReplyAsync($"Help me, {user.Username} {emojiBad}, AM SUFFERING FROM");
                await ReplyAsync($"restart");
            }
            await ReplyAsync($"Thanks for asking {user.Username}! I'm doing great {emojiGood}");
        }

        [Command("is")]
        public async Task IsInsult([Remainder] string message)
        {
            var user = Context.User;
            var msg = message.ToLower();
            string[] compliments = new string[] {
                "smart",
                "cool",
                "fast",
                "amazing",
                "good",
                "awesome",
                "superb",
                "supercalifragilisticexpialidocious",
                "optimised",
                "perfect",
                "powerful",
            };
            if (msg.Contains("not"))
            {
                foreach (var trigger in compliments)
                {
                    if (message.Contains(trigger))
                    {
                        await ReplyAsync($"{user.Username} no u");
                        return;
                    }
                }
                return;
            }
            string[] triggers = new string[] {
                "dumb",
                "stinky",
                "stupid",
                "weird",
                "broken",
                "buggy",
                "slow",
                "sluggish",
                "unoptimised",
                "glitchy"
            };
            foreach (var trigger in triggers)
            {
                if (message.Contains(trigger))
                {
                    await ReplyAsync($"{user.Username} no u");
                    return;
                }
            }

        }
        
    }
}
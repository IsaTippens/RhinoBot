using System.Threading.Tasks;
using System;
using System.Text;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;

using RhinoBot.Models;
using RhinoBot.Core.Services;
public class ListModule : InteractiveBase
{   
    [Group("add")]
    public class AddItem : InteractiveBase
    {

        public JsonService jsonService { get; set; }

        [Command("grudge", RunMode = RunMode.Async)]
        [Alias("grudges")]
        public async Task AddGrudgeAsync([Remainder] string title = null)
        {
            var msg = Context.Message;
            if (title == null)
            {
                await ReplyAsync("What should we name the grudge?");
                var reply = await NextMessageAsync(timeout: TimeSpan.FromMinutes(1.0));
                if (reply == null) 
                {
                    await ReplyAsync($"<@{Context.User.Id}> took too long to respond");
                    return;
                }
                title = reply.Content;
            }
            await ReplyAsync($"Reply with the entry for {title}");
            var response = await NextMessageAsync(timeout: TimeSpan.FromMinutes(5.0));
            if (response != null)
            {
                var author = response.Author;

                var grudges = await jsonService.ReadAsync<Dictionary<string, string>>("Data/grudges.json");
                if (grudges == null)
                {
                    grudges = new Dictionary<string, string>();
                }
                bool edited = grudges.ContainsKey(title);

                Embed embed;
                if (edited)
                {
                    var old = grudges[title];
                    embed = EditEntry(author, title, response.Content, old);
                    grudges[title] = response.Content;
                }
                else
                {
                    embed = NewEntry(author, title, response.Content);
                    grudges.Add(title, response.Content);
                }
                await jsonService.WriteAsync<Dictionary<string, string>>(grudges, "Data/grudges.json");
                await ReplyAsync(embed: embed);
            }
            else
            {
                await ReplyAsync($"You're too slow!");
            }
        }

        private Embed NewEntry(SocketUser user, string title, string description)
        {
            var grudge = new EmbedFieldBuilder().WithName(title).WithValue(description);

            return new EmbedBuilder()
            .WithTitle("Book of Grudges")
            .WithAuthor(user.Username, user.GetAvatarUrl())
            .WithFields(grudge)
            .Build();
        }
        private Embed EditEntry(SocketUser user, string title, string description, string oldDescription)
        {
            var grudge = new EmbedFieldBuilder().WithName("Edited").WithValue(title);
            var before = new EmbedFieldBuilder().WithName("Before").WithValue(oldDescription);
            var after  = new EmbedFieldBuilder().WithName("After").WithValue(description);

            return new EmbedBuilder()
            .WithTitle("Book of Grudges")
            .WithAuthor(user.Username, user.GetAvatarUrl())
            .WithFields(grudge)
            .WithFields(before)
            .WithFields(after)
            .Build();

        }
    }

    [RequireOwner()]
    [Group("delete")]
    [Alias("remove")]
    public class DeleteItems : InteractiveBase
    {
        public JsonService jsonService { get; set; }

        [Command("grudge", RunMode = RunMode.Async)]
        [Alias("grudges")]
        public async Task ListGrudgesAsync([Remainder]string extra = null)
        {
            var grudges = await jsonService.ReadAsync<Dictionary<string, string>>("Data/grudges.json");
            if (extra == null)
            {
                await ReplyAsync("Which grudge would you like to remove?");
                var response = await NextMessageAsync();
                if (response == null)
                {
                    await ReplyAsync($"<@{Context.User.Id}> took too long to respond");
                    return;
                }
                extra = response.Content;
            }
            if (grudges.ContainsKey(extra)) 
            {
                var msg = Context.Message;
                grudges.Remove(extra);
                await jsonService.WriteAsync<Dictionary<string, string>>(grudges, "Data/grudges.json");
                await msg.AddReactionAsync(new Emoji("✅"));
            }
            else
            {
                await ReplyAsync($"No grudge exists for {extra}");
            }
        }
    }

    [Group("list")]
    [Alias("display")]
    public class ListItems : InteractiveBase
    {
        public JsonService jsonService { get; set; }

        [Command("grudge")]
        [Alias("grudges")]
        public async Task ListGrudgesAsync([Remainder] string extra = null)
        {
            var grudges = await jsonService.ReadAsync<Dictionary<string, string>>("Data/grudges.json");
            if (grudges == null || grudges.Count == 0)
            {
                await ReplyAsync("```Book of Grudges\nNone!```");
            }
            else
            {
                StringBuilder sb = new StringBuilder("```Book of Grudges\n\n");
                foreach (string key in grudges.Keys)
                {
                    sb.Append($"{key}: {grudges[key]}\n\n");
                }
                sb.Append("```");
                await ReplyAsync(sb.ToString());
            }
        }
    }
}
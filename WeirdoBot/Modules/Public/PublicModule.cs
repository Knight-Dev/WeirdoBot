using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using WeirdoBot.util;
using WeirdoBot.Config;
using System.Threading.Tasks;

namespace WeirdoBot.Modules.Public
{
    public class PublicModule : ModuleBase
    {

        // rules list

        [Command("rules")]
        public async Task DisplayRulesAsync()
        {
            var embed = new EmbedBuilder()
            {
                Color = Colors.generalCol
            };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            var blank = new EmbedFieldBuilder() { Name = "\u200b", Value = "\u200b" };
            var rule1Field = new EmbedFieldBuilder() { Name = "1. Community Respect", Value = "\u200b"};
            var rule2Field = new EmbedFieldBuilder() { Name = "2. Keep Chat PG13", Value = "\u200b" };
            var rule3Field = new EmbedFieldBuilder() { Name = "3. Do Not Use Excessive Swear Words", Value = "\u200b" };
            var rule4Field = new EmbedFieldBuilder() { Name = "4. Do Not Spam Chat With The Same Messages", Value = "\u200b" };
            var rule5Field = new EmbedFieldBuilder() { Name = "5. Do Not Advertise Other Servers", Value = "\u200b" };
            var rule6Field = new EmbedFieldBuilder() { Name = "6. Do Not Send Random/Unwanted Links Or Inappropriate Links", Value = "\u200b" };
            var rule7Field = new EmbedFieldBuilder() { Name = "7. No Griefing", Value = "\u200b" };
            var rule8Field = new EmbedFieldBuilder() { Name = "8. Do Not Ask For Ranks Or Items", Value = "\u200b" };
            var rule9Field = new EmbedFieldBuilder() { Name = "9. Do Not Threaten To Get Someone Banned", Value = "\u200b" };
            var rule10Field = new EmbedFieldBuilder() { Name = "10. Do Not Follow New Players Around With An Invis Potion etc.", Value = "\u200b" };
            var rule11Field = new EmbedFieldBuilder() { Name = "11. Do Not Tell A New Player To Read The Rules", Value = "\u200b" };
            var rule12Field = new EmbedFieldBuilder() { Name = "12. Hacks Are **NOT** Allowed (ex: Fly Hacking, Nuke Hacking, X-Ray)", Value = "\u200b" };
            var rule13Field = new EmbedFieldBuilder() { Name = "13. Do Not Use A Resource Pack That Affects Your Game To Help You Find Things", Value = "\u200b" };

            embed.WithFooter(footer);
            embed.Title = $"**Rules**";
            embed.Description = $"General Server Rules";
            embed.WithThumbnailUrl("https://s3.amazonaws.com/files.enjin.com/1123262/weirdo%20craft%20Small.png");
            //embed.AddField(blank);
            embed.AddField(rule1Field);
            embed.AddField(rule2Field);
            embed.AddField(rule3Field);
            embed.AddField(rule4Field);
            //embed.AddField(blank);
            embed.AddField(rule5Field);
            embed.AddField(rule6Field);
            embed.AddField(rule7Field);
            embed.AddField(rule8Field);
            //embed.AddField(blank);
            embed.AddField(rule9Field);
            embed.AddField(rule10Field);
            embed.AddField(rule11Field);
            embed.AddField(rule12Field);
            //embed.AddField(blank);
            embed.AddField(rule13Field);
            //embed.AddField(blank);

            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        // vote links

        [Command("vote")]
        public async Task DisplayVoteLinksAsync()
        {
            var embed = new EmbedBuilder()
            {
                Color = Colors.generalCol
            };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            var blank = new EmbedFieldBuilder() { Name = "\u200b", Value = "\u200b" };
            var link1Field = new EmbedFieldBuilder() { Name = "Link 1", Value = "http://minecraftservers.org/vote/91258" };
            var link2Field = new EmbedFieldBuilder() { Name = "Link 2", Value = "http://minecraft-mp.com/server/104507/vote/" };

            embed.WithFooter(footer);
            embed.Title = $"**Vote Links**";
            embed.Description = $"Want To Support The Server? Vote To Get It On The Top!";
            embed.WithThumbnailUrl("https://s3.amazonaws.com/files.enjin.com/1123262/weirdo%20craft%20Small.png");
            embed.AddField(link1Field);
            embed.AddField(link2Field);

            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        // website info/ links

        [Command("links")]
        public async Task DisplayLinksAsync()
        {
            var embed = new EmbedBuilder()
            {
                Color = Colors.generalCol
            };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            var blank = new EmbedFieldBuilder() { Name = "\u200b", Value = "\u200b" };
            var link1Field = new EmbedFieldBuilder() { Name = "Website", Value = "http://www.weirdocraft.com/" };
            var link2Field = new EmbedFieldBuilder() { Name = "Rules", Value = "Do !rules" };
            var link3Field = new EmbedFieldBuilder() { Name = "Donate", Value = "http://weirdocraft.com/shop" };
            var link4Field = new EmbedFieldBuilder() { Name = "Report Someone", Value = "http://www.weirdocraft.com/report" };

            embed.WithFooter(footer);
            embed.Title = $"**Links**";
            embed.Description = $"General Links For The Server";
            embed.WithThumbnailUrl("https://s3.amazonaws.com/files.enjin.com/1123262/weirdo%20craft%20Small.png");
            embed.AddField(link1Field);
            embed.AddField(link2Field);
            embed.AddField(link3Field);
            embed.AddField(link4Field);

            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("", false, embed);
        }

    }
}

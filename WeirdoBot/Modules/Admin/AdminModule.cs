using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using WeirdoBot.util;
using WeirdoBot.Config;
using System.Linq;

namespace WeirdoBot.Modules.Admin
{
    public class AdminModule : ModuleBase<SocketCommandContext>
    {

        Errors errors = new Errors();
        BotConfig config = new BotConfig();


        [Command("clear")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Cl([Remainder] int x = 0)
        {

            if (x <= 0)
            {
                var embed = new EmbedBuilder() { Color = Colors.adminCol };
                embed.Title = ("**Clear Messages**");
                embed.Description = ($"**{Context.User.Mention}**, You Cannot Delete **0** Messages!");
                await ReplyAsync("", false, embed.Build());
            }
            else if (x <= 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(x + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);

                var embed = new EmbedBuilder() { Color = Colors.adminCol };
                embed.Title = ("**Clear Messages**");
                embed.Description = ($"**{Context.User.Mention}** Deleted **{x}** Messages.");
                await ReplyAsync("", false, embed.Build());
            }
            else if (x > 100)
            {
                var embed = new EmbedBuilder() { Color = Colors.adminCol };
                embed.Title = ("**Clear Messages**");
                embed.Description = ("**{Context.User.Mention}**, You Cannot Delete More Than 100 Messages!");
                await ReplyAsync("", false, embed.Build());
            }

        }


        [Command("ban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null) await errors.sendError(Context.Channel, "You must enter a user!", Colors.adminCol);
            if (string.IsNullOrWhiteSpace(reason)) await errors.sendError(Context.Channel, "You must enter a reason!", Colors.adminCol);

            var gld = Context.Guild as SocketGuild;
            var embed = new EmbedBuilder()
            {
                Color = Colors.adminCol
            };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            embed.WithFooter(footer);
            embed.Title = $"**{user.Username}** has been banned!";
            embed.Description = $"**Username: **{user.Username}\n**Guild Name: **{user.Guild.Name}\n**Banned By: **{Context.User.Mention}!\n**Reason: **{reason}";

            await gld.AddBanAsync(user);

            await Context.Message.DeleteAsync();

            var logchannel = Context.Guild.GetChannel(437977945680773130) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);

        }

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null) await errors.sendError(Context.Channel, "You must enter the user!", Colors.adminCol);
            if (string.IsNullOrWhiteSpace(reason)) await errors.sendError(Context.Channel, "You must enter a reason!", Colors.adminCol);

            var gld = Context.Guild as SocketGuild;
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            embed.WithFooter(footer);
            embed.Title = $"**{user.Username}** has been kicked from **{user.Guild.Name}**!";
            embed.Description = $"**Username: **{user.Username}\n**Guild name: **{user.Guild.Name}\n**Kicked By: **{Context.User.Mention}\n**Reason: **{reason}";

            await user.KickAsync();

            await Context.Message.DeleteAsync();

            var logchannel = Context.Guild.GetChannel(437977945680773130) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }


        [Command("addrole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task AddRoleAsync(IGuildUser user, [Remainder]string roleTobe)
        {
            if (user.Equals(null)) await errors.sendError(Context.Channel, "You Must Enter A User!", Colors.adminCol);
            if (string.IsNullOrWhiteSpace(roleTobe)) await errors.sendError(Context.Channel, "You Must Enter A Role!", Colors.adminCol);

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == roleTobe);
            await (user as IGuildUser).AddRoleAsync(role);

            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            embed.WithFooter(footer);
            embed.Title = $"**{user.Username}** has been added to **{roleTobe}**!";
            embed.Description = $"**Username: **{user.Username}\n**Guild name: **{user.Guild.Name}\n**Role: **{roleTobe}";

            var logchannel = Context.Guild.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);

        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task AddMutedRoleAsync(IGuildUser user)
        {
            if (user.Equals(null)) await errors.sendError(Context.Channel, "You Must Enter A User!", Colors.adminCol);

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "MUTED");
            await (user as IGuildUser).AddRoleAsync(role);

            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            embed.WithFooter(footer);
            embed.Title = $"**{user.Username}** Has Been Muted!";
            embed.Description = $"Now Why Did Ya Have To Do That?";

            var logchannel = Context.Guild.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);

        }

        [Command("remrole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task RemRoleAsync(IGuildUser user, [Remainder]string roleTobe)
        {
            if (user.Equals(null)) await errors.sendError(Context.Channel, "You Must Enter A User!", Colors.adminCol);
            if (string.IsNullOrWhiteSpace(roleTobe)) await errors.sendError(Context.Channel, "You Must Enter A Role!", Colors.adminCol);

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == roleTobe);
            await (user as IGuildUser).RemoveRoleAsync(role);

            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute };

            embed.WithFooter(footer);
            embed.Title = $"**{user.Username}** has been removed from **{roleTobe}**!";
            embed.Description = $"**Username: **{user.Username}\n**Guild name: **{user.Guild.Name}\n**Role: **{roleTobe}";

            var logchannel = Context.Guild.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);

        }

    }
}

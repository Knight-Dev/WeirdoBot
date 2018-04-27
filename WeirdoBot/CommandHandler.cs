using System.Threading.Tasks;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WeirdoBot.Config;
using WeirdoBot.Modules.Public;
using WeirdoBot.util;
using System.IO;
using Discord.Rest;
using System.Threading;
using System.Timers;
using WeirdoBot.Modules.Admin;
using WeirdoBot.Modules.Profanity;
using WeirdoBot.Modules.Reports;

namespace WeirdoBot
{
    public class CommandHandler : ModuleBase
    {
        private CommandService commands;
        private static DiscordSocketClient bot;
        private IServiceProvider map;

        

        public static readonly string appdir = AppContext.BaseDirectory;

        public CommandHandler(IServiceProvider provider)
        {
            map = provider;
            bot = map.GetService<DiscordSocketClient>();
            bot.UserJoined += AnnounceUserJoined;
            bot.UserLeft += AnnounceLeftUser;
            bot.Ready += SetGame;
            //Send user message to get handled
            bot.MessageReceived += HandleCommand;
            commands = map.GetService<CommandService>();
            // Start Logs Lmao
            bot.ChannelCreated += ChannelCreatedAsync;
            bot.ChannelDestroyed += ChannelDeletedAsync;
            bot.RoleCreated += RoleCreatedAsync;
            bot.RoleDeleted += RoleDeletedAsync;
            bot.UserBanned += BannedUserAsync;
            bot.UserUnbanned += UnBannedUserAsync;
            bot.GuildUpdated += GuildUpdatedAsync;
            bot.MessageUpdated += MessageUpdatedAsync;
            // End Logs Lmao
            //start msg received stuff
            bot.MessageReceived += Reports.HandleReportAsync;
            bot.MessageReceived += ProfanityFilter.ProfanityCheckAsync;
            //end msg recieved stff
        }

        public async Task UserUpdatedAsync(SocketUser user, SocketUser usr)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**A User Has Been Updated**");
            embed.Description = ("Username: " + user.Username + "\nUser Id: " + user.Id + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task MessageUpdatedAsync(Cacheable<IMessage, ulong> msgid, SocketMessage msg, ISocketMessageChannel chnl)
        {

            var aftermsg = await msgid.GetOrDownloadAsync();

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Edited A Message**");
            embed.Description = ("Username: " + msg.Author + "\nNew Message: " + aftermsg + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task BotUpdatedAsync(SocketSelfUser usr, SocketSelfUser user)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**The Bot Has Been Updated**");
            embed.Description = ("Bot Name: " + usr.Username + "\nBot Id: " + usr.Id + "\nBot Game: " + usr.Game + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task GuildUpdatedAsync(SocketGuild gld, SocketGuild guld)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**The Guild Has Been Updated**");
            embed.Description = ("Guild Name: " + gld.Name + "\nGuild Id: " + gld.Id + "\nMember Amount: " + gld.MemberCount + "\nGuild Owner: " + gld.Owner + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task BannedUserAsync(SocketUser usr, SocketGuild gld)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Has Been Banned From The Server**");
            embed.Description = ("Username: " + usr + "\nUser Id: " + usr.Id + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task UnBannedUserAsync(SocketUser usr, SocketGuild gld)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Has Been UnBanned From The Server**");
            embed.Description = ("Username: " + usr + "\nUser Id: " + usr.Id + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task RoleCreatedAsync(SocketRole role)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Created A Role**");
            embed.Description = ("Role Name: " + role.Name + "\nRole Id: " + role.Id + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task RoleDeletedAsync(SocketRole role)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Deleted A Role**");
            embed.Description = ("Role Name: " + role.Name + "\nRole Id: " + role.Id + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task ChannelCreatedAsync(SocketChannel chnl)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Created A Channel**");
            embed.Description = ("\nChannel Name: " + chnl + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task ChannelDeletedAsync(SocketChannel chnl)
        {
            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Deleted A Channel**");
            embed.Description = ("\nChannel Name: " + chnl + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);
        }

        public async Task AnnounceLeftUser(SocketGuildUser user)
        {

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Left The Discord**");
            embed.Description = ("Username: " + user.Username + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\nTotal Members: " + bot.GetGuild(BotConfig.Load().serverId).MemberCount.ToString());
            embed.WithFooter(footer);

            var logchannel = bot.GetChannel(BotConfig.Load().LogChannel) as SocketTextChannel;
            await logchannel.SendMessageAsync("", false, embed);

        }

        public async Task AnnounceUserJoined(SocketGuildUser user)
        {

            var server = bot.Guilds.FirstOrDefault(x => x.Id == BotConfig.Load().serverId);
            var guild = server as IGuild;
            await user.AddRoleAsync(guild.Roles.FirstOrDefault(x => x.Name == BotConfig.Load().NewMemberRank));

            var embed = new EmbedBuilder() { Color = Colors.adminCol };
            var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " | " + "KnightDev.xyz" };
            embed.Title = ("**User Joined The Discord**");
            embed.Description = ("Username: " + user.Username + "\nTime: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\nTotal Members: " + bot.GetGuild(BotConfig.Load().serverId).MemberCount.ToString());
            embed.WithFooter(footer);

        }

        public async Task SetGame()
        {
            await bot.SetGameAsync(BotConfig.Load().Prefix + "help" + " | " + "KnightDev.xyz");
        }




        public async Task ConfigureAsync()
        {
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage pMsg)
        {


            //Don't handle the command if it is a system message
            var message = pMsg as SocketUserMessage;
            if (message == null)
                return;
            var context = new SocketCommandContext(bot, message);

            //Mark where the prefix ends and the command begins
            int argPos = 0;
            //Determine if the message has a valid prefix, adjust argPos



            if (message.HasStringPrefix(BotConfig.Load().Prefix, ref argPos))
            {
                if (message.Author.IsBot)
                    return;
                //Execute the command, store the result
                var result = await commands.ExecuteAsync(context, argPos, map);

                //If the command failed, notify the user
                if (!result.IsSuccess && result.ErrorReason != "Unknown command.")
                {
                    var embed = new EmbedBuilder() { Color = Colors.errorcol };
                    var footer = new EmbedFooterBuilder() { Text = "WeirdoBot By KnightDev" + " | " + DateTime.Today + DateTime.Now };
                    embed.Title = ("**Error**");
                    embed.Description = ($"{result.ErrorReason}");
                    embed.WithFooter(footer);
                    await message.Channel.SendMessageAsync("", false, embed);
                }
            }
        }

        public static DiscordSocketClient GetBot() { return bot; }

    }
}

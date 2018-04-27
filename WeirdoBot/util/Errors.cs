using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace WeirdoBot.util
{
    class Errors : ModuleBase<SocketCommandContext>
    {
        public async Task sendError(ISocketMessageChannel channel, string error, Color color)
        {
            //Console.WriteLine("ERROR: " + error);

            var embed = new EmbedBuilder() { Color = color };
            embed.Title = ("ERROR");
            embed.Description = (error);
            await channel.SendMessageAsync("", false, embed);

            Console.WriteLine("Error message was sent to the user.");

        }

        public async Task sendError(IMessageChannel channel, string error, Color color)
        {
            //Console.WriteLine("ERROR: " + error);

            var embed = new EmbedBuilder() { Color = color };
            embed.Title = ("ERROR");
            embed.Description = (error);
            await channel.SendMessageAsync("", false, embed);

            Console.WriteLine("Error message was sent to the user.");

        }

        public async Task sendErrorTempAsync(ISocketMessageChannel channel, string error, Color color)
        {
            await Program.Logger(new LogMessage(LogSeverity.Error, "WeirdoBot Errors", error));
            var embed = new EmbedBuilder() { Color = color };
            embed.Title = ("ERROR");
            embed.Description = (error);
            var errorMessage = await channel.SendMessageAsync("", false, embed);
            await Delete.DelayDeleteMessage(errorMessage, 30);
        }

    }
}

using Discord.WebSocket;
using WeirdoBot.Config;
using WeirdoBot.util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeirdoBot.Modules.Reports
{
    class Reports
    {

        public static async Task HandleReportAsync(SocketMessage pMsg)
        {
            Errors errors = new Errors();
            var message = pMsg as SocketUserMessage;
            var user = message.Author;

            if (message == null)
                return;
            if (user.IsBot)
                return;
            if (message.Channel.Id != StaffConfig.Load().ReportsChnl)
                return;
            if (ReportChecks.IsStaff(user) == true)
                return;
            if (ReportChecks.IsCorrectLayout(pMsg.ToString()) == true)
                return;

            await errors.sendErrorTempAsync(message.Channel, message.Author.Mention + " please use the correct message layout, it is pinned in this channel!", Colors.errorcol);
            var iDMChannel = await user.GetOrCreateDMChannelAsync();
            await iDMChannel.SendMessageAsync("Here is a copy of your report that was the wrong layout.\n```\n" + pMsg.ToString() + "\n```\nPlease use the layout that is pinned in the bug reports channel!");
            await pMsg.DeleteAsync();

        }
    }
}

using Discord;
using System.Threading.Tasks;

namespace WeirdoBot.util
{
    class Delete
    {
        public static async Task DelayDeleteMessage(IUserMessage message, int time = 0)
        {
            var delete = Task.Run(async () =>
            {
                if (time == 0) await Task.Delay(2500);
                else await Task.Delay(time * 1000);

                await message.DeleteAsync();
            });
        }
    }
}

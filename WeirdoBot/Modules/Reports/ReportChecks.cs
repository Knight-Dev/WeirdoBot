
using Discord;
using WeirdoBot.Config;

namespace WeirdoBot.Modules.Reports
{
    class ReportChecks
    {

        public static bool IsStaff(IUser user)
        {
            for (int i = 0; i <= StaffConfig.Load().Staff - 1; i++)
            {
                if (StaffConfig.Load().StaffMembers[i] == user.Id)
                {
                    Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Dev Reports", "This isn't a report, it is a dev!"));
                    return true;
                }
            }
            Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Dev Reports", "This isn't a dev, it is a report!"));
            return false;
        }

        public static bool IsCorrectLayout(string message)
        {
            if (message.ToLower().Contains("person:"))
            {
                if (message.ToLower().Contains("rule broken:"))
                {
                    if (message.ToLower().Contains("describe:"))
                    {
                        Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Dev Reports", "This report is the correct layout!"));
                        return true;
                    }
                }
            }
            Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Dev Reports", "This report is not the correct layout!"));
            return false;
        }

    }
}

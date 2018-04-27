using Discord;
using Newtonsoft.Json;
using System;
using System.IO;

namespace WeirdoBot.Config
{
    class StaffConfig
    {

        [JsonIgnore]
        public static readonly string appdir = AppContext.BaseDirectory;

        public ulong ReportsChnl { get; set; }
        public int Staff { get; set; }
        public ulong[] StaffMembers { get; set; }

        public StaffConfig()
        {
            ReportsChnl = 0;
            Staff = 0;
            StaffMembers = new ulong[15];
        }

        public void Save(string dir = "configuration/report_config.json")
        {
            string file = Path.Combine(appdir, dir);
            File.WriteAllText(file, ToJson());
            Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Configuration", "Dev configuration saved successfully."));
        }
        public static StaffConfig Load(string dir = "configuration/report_config.json")
        {
            string file = Path.Combine(appdir, dir);
            return JsonConvert.DeserializeObject<StaffConfig>(File.ReadAllText(file));
            Program.Logger(new LogMessage(LogSeverity.Info, "WeirdoBot Configuration", "Dev configuration loaded successfully."));
        }
        public string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);

    }
}

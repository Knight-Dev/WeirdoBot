using Newtonsoft.Json;
using System;
using System.IO;

namespace WeirdoBot.Config
{
    public class BotConfig
    {
        [JsonIgnore]
        public static readonly string appdir = AppContext.BaseDirectory;

        public ulong serverId { get; set; }
        public string Prefix { get; set; }
        public string Token { get; set; }
        public string NewMemberRank { get; set; }
        public ulong LogChannel { get; set; }
        public int Filters { get; set; }
        public string[] FilteredWords { get; set; }
        public BotConfig()
        {
            serverId = 0;
            Prefix = "!";
            Token = "";
            NewMemberRank = "";
            LogChannel = 439144444537602060;
            Filters = 2;
            FilteredWords = new string[20];
        }

        public void Save(string dir = "configuration/config.json")
        {
            string file = Path.Combine(appdir, dir);
            File.WriteAllText(file, ToJson());
        }
        public static BotConfig Load(string dir = "configuration/config.json")
        {
            string file = Path.Combine(appdir, dir);
            return JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(file));
        }
        public string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);

    }
}

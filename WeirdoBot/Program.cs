﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Collections.Generic;
using WeirdoBot.Config;

namespace WeirdoBot
{
    public class Program
    {
        public static List<ulong> modRoleID = new List<ulong>();
        public static ulong[] modRoleIDs;
        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task Start()
        {
            EnsureBotConfigExists(); // Ensure that the bot configuration json file has been created.

            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
            });

            client.Log += Logger;
            await client.LoginAsync(TokenType.Bot, BotConfig.Load().Token);
            await client.StartAsync();

            var serviceProvider = ConfigureServices();
            handler = new CommandHandler(serviceProvider);
            await handler.ConfigureAsync();

            //Block this program untill it is closed
            await Task.Delay(-1);
        }
        public static Task Logger(LogMessage lmsg)
        {
            var cc = Console.ForegroundColor;
            switch (lmsg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            Console.WriteLine($"{DateTime.Now} [{lmsg.Severity,8}] {lmsg.Source}: {lmsg.Message}");
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }

        public static void EnsureBotConfigExists()
        {
            if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "configuration")))
                Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "configuration"));

            string loc = Path.Combine(AppContext.BaseDirectory, "configuration/config.json");
            string staffloc = Path.Combine(AppContext.BaseDirectory, "configuration/report_config.json");

            if (!File.Exists(loc))                              // Check if the configuration file exists.
            {
                var config = new BotConfig();               // Create a new configuration object.


                Console.WriteLine("Please enter the following information to save into your configuration/config.json file");

                Console.Write("Bot Token: ");
                config.Token = Console.ReadLine();              // Read the bot token from console.

                Console.Write("Bot Prefix: ");
                config.Prefix = Console.ReadLine();              // Read the bot prefix from console.

                Console.Write("New Member Role: ");
                config.NewMemberRank = Console.ReadLine();

                config.Save();                                  // Save the new configuration object to file.
            }
            Console.WriteLine("Configuration has been loaded!");

            if (!File.Exists(staffloc))                              // Check if the configuration file exists.
            {
                var config = new StaffConfig();               // Create a new configuration object


                config.ReportsChnl = 0;
                config.Staff = 0;

                config.Save();                                  // Save the new configuration object to file.
            }
            Console.WriteLine("Report Config Has Been Loaded!");
        }
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton(client)
                 .AddSingleton(new CommandService(new CommandServiceConfig { CaseSensitiveCommands = false }));
            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);

            return provider;
        }
    }
}

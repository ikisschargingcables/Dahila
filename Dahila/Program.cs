using Discord.Commands;
using Discord.WebSocket;
using System;
using Discord;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AvEmUpdate
{
    internal class Program
    {
        DiscordSocketClient _client;
        CommandService _service;

        private static void Main()
        {
            Console.Title = "AvEmUpdate";
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            try
            {
                var process = Process.GetProcessesByName("AvEmUpdate"); if(process.Length > 1) { Process.GetCurrentProcess().Kill(); }
                new Program().StartAsync().GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig {
                DefaultRetryMode = RetryMode.AlwaysRetry,
                LogLevel = LogSeverity.Debug
            });
            _client.Log += LogHandler;
            await _client.LoginAsync(TokenType.Bot, "NDcxNTY2NDY2MDk4MTM1MDYx.DjzE0A.KJRJKtwaGP2MITERKTMmmTXUGUY");
            await _client.StartAsync();
            await InitializeAsync();
            await Task.Delay(-1);
        }

        private async Task LogHandler(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            await Task.Delay(1);
        }

        public async Task InitializeAsync()
        {
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetExecutingAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if(msg.HasStringPrefix("!", ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}

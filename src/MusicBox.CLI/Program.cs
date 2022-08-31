using MusicBox.Domain.UrlParsing;
using MusicBox.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MusicBox.CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IAppServices appServices = AppServices.Create();
            
            Console.Write("Enter a link to a playlist or press Enter to quit: ");
            string? url = Console.ReadLine();

            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    PlaylistUrlInfo urlInfo = appServices.PlaylistUrlParser.ParseUrl(url);
                    PrintPlaylistUrlInfo(urlInfo);

                    if (urlInfo.IsAuthenticationRequired)
                    {
                        Console.WriteLine($"Authentication is required for {urlInfo.Service}. Please, provide your credentials.");
                        
                        Console.Write("Login: ");
                        string userName = Console.ReadLine() ?? "";

                        Console.Write("Password: ");
                        string password = Console.ReadLine() ?? "";

                        var authProcess = appServices.AuthProcessFactory.CreateAuthProcess(urlInfo.Service);
                        var authResult = await authProcess.SendCredentials(userName, password);

                        Console.WriteLine("[Info] Authentication result: ");
                        foreach (var key in authResult.Data.Keys)
                        {
                            Console.WriteLine($"   {key}:{authResult.Data[key]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                }

                Console.ReadLine();
            }
        }

        private static void PrintPlaylistUrlInfo(PlaylistUrlInfo urlInfo)
        {
            Console.WriteLine($"[Info] Service: {urlInfo.Service}");
            Console.WriteLine($"[Info] Playlist type: {urlInfo.PlaylistType}");
            foreach (var paramName in urlInfo.Params.Keys)
            {
                Console.WriteLine($"[Info] {paramName}:{urlInfo.Params[paramName]}");
            }
        }
    }
}
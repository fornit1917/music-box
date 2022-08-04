using MusicBox.Domain.Models;
using MusicBox.Infrastructure;
using System;

namespace MusicBox.CLI
{
    internal class Program
    {
        static void Main(string[] args)
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
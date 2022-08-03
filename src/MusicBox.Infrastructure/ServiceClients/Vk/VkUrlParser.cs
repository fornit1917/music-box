using MusicBox.Domain.Exceptions;
using MusicBox.Domain.Models;
using MusicBox.Domain.Services;
using MusicBox.Infrastructure.ServiceClients.Vk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicBox.Infrastructure.ServiceClients.Vk
{
    public class VkUrlParser : IServiceUrlParser
    {
        private static readonly IReadOnlyList<string> SupportedDomains = new[] { "vk.com", "m.vk.com" };

        private static readonly Regex AccountAudiosRegex = new Regex("audios(\\-{0,1}[0-9]+)");
        private static readonly Regex WallPostRegex = new Regex("wall(\\-{0,1}[0-9]+_[0-9]+)");
        private static readonly Regex PlaylistRegex = new Regex("audio_playlist(\\-{0,1}[0-9]+_[0-9]+)");
        private static readonly Regex PlaylistDirectRegex = new Regex("playlist/(\\-{0,1}[0-9]+_[0-9]+)");

        public bool CanParseUrl(Uri uri)
        {
            return SupportedDomains.Contains(uri.Host);
        }

        public PlaylistUrlInfo ParseUrl(Uri uri)
        {
            Match playlistMatch = PlaylistRegex.Match(uri.AbsoluteUri);
            if (!playlistMatch.Success)
            {
                playlistMatch = PlaylistDirectRegex.Match(uri.AbsoluteUri);
            }
            if (playlistMatch.Success)
            {
                return CreateVkPlaylistInfo(uri, playlistMatch.Groups[1].Value);
            }

            Match wallPostMatch = WallPostRegex.Match(uri.AbsoluteUri);
            if (wallPostMatch.Success)
            {
                return CreateWallPostInfo(uri, wallPostMatch.Groups[1].Value);
            }

            Match accountAudiosMatch = AccountAudiosRegex.Match(uri.AbsoluteUri);
            if (accountAudiosMatch.Success)
            {
                return CreateAccountAudiosInfo(uri, accountAudiosMatch.Groups[1].Value);
            }

            throw new PlaylistUrlParsingException($"It's not a link to supported VK music playlist: '{uri.AbsoluteUri}'");
        }

        private PlaylistUrlInfo CreateAccountAudiosInfo(Uri uri, string accountId)
        {
            var info = new PlaylistUrlInfo(uri, Service.Vk, PlaylistType.VkAccountAudios, isAuthenticationRequired: true);
            info.SetParam(VkUrlParams.AccountId, accountId);
            return info;
        }

        private PlaylistUrlInfo CreateWallPostInfo(Uri uri, string postId)
        {
            var info = new PlaylistUrlInfo(uri, Service.Vk, PlaylistType.VkPost, isAuthenticationRequired: true);
            info.SetParam(VkUrlParams.PostId, postId);
            return info;
        }

        private PlaylistUrlInfo CreateVkPlaylistInfo(Uri uri, string postId)
        {
            var info = new PlaylistUrlInfo(uri, Service.Vk, PlaylistType.VkPlaylist, isAuthenticationRequired: true);
            info.SetParam(VkUrlParams.PlaylistId, postId);
            return info;
        }
    }
}

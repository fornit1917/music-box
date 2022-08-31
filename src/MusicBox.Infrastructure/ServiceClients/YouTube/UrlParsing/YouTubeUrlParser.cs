using MusicBox.Domain;
using MusicBox.Domain.UrlParsing;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace MusicBox.Infrastructure.ServiceClients.YouTube.UrlParsing
{
    public class YouTubeUrlParser : IServiceUrlParser
    {
        private readonly string[] SupportedDomains = new[] { "youtube.com", "www.youtube.com" };

        public bool CanParseUrl(Uri uri)
        {
            return SupportedDomains.Contains(uri.Host.ToLower());
        }

        public PlaylistUrlInfo ParseUrl(Uri uri)
        {
            NameValueCollection? queryParams = HttpUtility.ParseQueryString(uri.Query);
            string? listId = queryParams.Get("list");
            string? videoId = queryParams.Get("v");

            if (!string.IsNullOrEmpty(listId) && !string.IsNullOrEmpty(videoId))
            {
                return CreatePlaylistInfo(uri, listId, videoId);
            }

            if (!string.IsNullOrEmpty(videoId))
            {
                return CreateVideoInfo(uri, videoId);
            }

            throw new PlaylistUrlParsingException($"It's not a link to supported YouTube playlist: {uri.AbsoluteUri}");
        }

        private PlaylistUrlInfo CreatePlaylistInfo(Uri uri, string listId, string videoId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YouTube, PlaylistType.YouTubePlaylist, isAuthenticationRequired: false);
            info.SetParam(YouTubeUrlParams.ListId, listId);
            info.SetParam(YouTubeUrlParams.VideoId, videoId);
            return info;
        }

        private PlaylistUrlInfo CreateVideoInfo(Uri uri, string videoId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YouTube, PlaylistType.YouTubeVideo, isAuthenticationRequired: false);
            info.SetParam(YouTubeUrlParams.VideoId, videoId);
            return info;
        }
    }
}

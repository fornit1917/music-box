using MusicBox.Domain.Exceptions;
using MusicBox.Domain.Models;
using MusicBox.Domain.Services;
using MusicBox.Infrastructure.ServiceClients.YandexMusic.Models;
using System;
using System.Text.RegularExpressions;

namespace MusicBox.Infrastructure.ServiceClients.YandexMusic
{
    public class YandexUrlParser : IServiceUrlParser
    {
        private static readonly Regex TrackRegex = new Regex("/album/([^/]+)/track/([^/]+)/?$");
        private static readonly Regex AlbumRegex = new Regex("/album/([^/]+)/?$");
        private static readonly Regex ArtistRegex = new Regex("/artist/([^/]+)/?$");
        private static readonly Regex PlaylistRegex = new Regex("/users/([^/]+)/playlists/([^/]+)/?$");
        private static readonly Regex UserAllRegex = new Regex("/users/([^/]+)/tracks/?$");

        public bool CanParseUrl(Uri uri)
        {
            return uri.Host == "music.yandex.ru";
        }

        public PlaylistUrlInfo ParseUrl(Uri uri)
        {
            Match trackMatch = TrackRegex.Match(uri.AbsolutePath);
            if (trackMatch.Success)
            {
                return CreateTrackInfo(uri, trackMatch.Groups[1].Value, trackMatch.Groups[2].Value);
            }

            Match albumMatch = AlbumRegex.Match(uri.AbsolutePath);
            if (albumMatch.Success)
            {
                return CreateAlbumInfo(uri, albumMatch.Groups[1].Value);
            }

            Match artistMatch = ArtistRegex.Match(uri.AbsolutePath);
            if (artistMatch.Success)
            {
                return CreateArtistInfo(uri, artistMatch.Groups[1].Value);
            }

            Match playlistMatch = PlaylistRegex.Match(uri.AbsolutePath);
            if (playlistMatch.Success)
            {
                return CreatePlaylistInfo(uri, playlistMatch.Groups[1].Value, playlistMatch.Groups[2].Value);
            }

            Match userAllMatch = UserAllRegex.Match(uri.AbsolutePath);
            if (userAllMatch.Success)
            {
                return CreateUserAllInfo(uri, userAllMatch.Groups[1].Value);
            }

            throw new PlaylistUrlParsingException($"It's not a link to supported YandexMusic playlist: {uri.AbsoluteUri}");
        }

        private PlaylistUrlInfo CreateTrackInfo(Uri uri, string albumId, string trackId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YandexMusic, PlaylistType.YandexTrack, isAuthenticationRequired: true);
            info.SetParam(YandexUrlParams.AlbumId, albumId);
            info.SetParam(YandexUrlParams.TrackId, trackId);
            return info;
        }

        private PlaylistUrlInfo CreateAlbumInfo(Uri uri, string albumId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YandexMusic, PlaylistType.YandexAlbum, isAuthenticationRequired: true);
            info.SetParam(YandexUrlParams.AlbumId, albumId);
            return info;
        }

        private PlaylistUrlInfo CreateArtistInfo(Uri uri, string artistId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YandexMusic, PlaylistType.YandexArtist, isAuthenticationRequired: true);
            info.SetParam(YandexUrlParams.ArtistId, artistId);
            return info;
        }

        private PlaylistUrlInfo CreatePlaylistInfo(Uri uri, string userId, string playlistId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YandexMusic, PlaylistType.YandexPlaylist, isAuthenticationRequired: true);
            info.SetParam(YandexUrlParams.UserId, userId);
            info.SetParam(YandexUrlParams.PlaylistId, playlistId);
            return info;
        }

        private PlaylistUrlInfo CreateUserAllInfo(Uri uri, string userId)
        {
            var info = new PlaylistUrlInfo(uri, Service.YandexMusic, PlaylistType.YandexUserAll, isAuthenticationRequired: true);
            info.SetParam(YandexUrlParams.UserId, userId);
            return info;
        }
    }
}

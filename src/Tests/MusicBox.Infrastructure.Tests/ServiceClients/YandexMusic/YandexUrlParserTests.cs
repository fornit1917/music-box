using MusicBox.Domain;
using MusicBox.Domain.UrlParsing;
using MusicBox.Infrastructure.ServiceClients.YandexMusic.UrlParsing;
using System;

namespace MusicBox.Infrastructure.Tests.ServiceClients.YandexMusic
{
    public class YandexUrlParserTests
    {
        private readonly YandexUrlParser _yandexUrlParser = new YandexUrlParser();

        [Theory]
        [InlineData("https://music.yandex.ru/album/123", true)]
        [InlineData("https://www.music.yandex.ru/album/123", true)]
        [InlineData("https://vk.com/album/123", false)]
        public void ReturnsCanParseFlag(string url, bool expectedResult)
        {
            var uri = new Uri(url);
            _yandexUrlParser.CanParseUrl(uri).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("https://music.yandex.ru/users/USER_ID/tracks", "USER_ID")]
        [InlineData("https://music.yandex.ru/users/USER_ID/tracks/", "USER_ID")]
        public void ParsesUserAllTracksUrl(string url, string expectedUserId)
        {
            var uri = new Uri(url);

            var result = _yandexUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YandexUserAll);
            result.GetParam(YandexUrlParams.UserId).Should().Be(expectedUserId);
        }

        [Theory]
        [InlineData("https://music.yandex.ru/users/USER_ID/playlists/PLAYLIST_ID", "USER_ID", "PLAYLIST_ID")]
        [InlineData("https://music.yandex.ru/users/USER_ID/playlists/PLAYLIST_ID/", "USER_ID", "PLAYLIST_ID")]
        public void ParsesPlaylistUrl(string url, string expectedUserId, string expectedPlaylistId)
        {
            var uri = new Uri(url);

            var result = _yandexUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YandexPlaylist);
            result.GetParam(YandexUrlParams.UserId).Should().Be(expectedUserId);
            result.GetParam(YandexUrlParams.PlaylistId).Should().Be(expectedPlaylistId);
        }

        [Theory]
        [InlineData("https://music.yandex.ru/artist/ARTIST_ID", "ARTIST_ID")]
        [InlineData("https://music.yandex.ru/artist/ARTIST_ID/", "ARTIST_ID")]
        public void ParsesArtistUrl(string url, string expectedArtistId)
        {
            var uri = new Uri(url);

            var result = _yandexUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YandexArtist);
            result.GetParam(YandexUrlParams.ArtistId).Should().Be(expectedArtistId);
        }

        [Theory]
        [InlineData("https://music.yandex.ru/album/ALBUM_ID", "ALBUM_ID")]
        [InlineData("https://music.yandex.ru/album/ALBUM_ID/", "ALBUM_ID")]
        public void ParsesAlbumUrl(string url, string expectedAlbumId)
        {
            var uri = new Uri(url);

            var result = _yandexUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YandexAlbum);
            result.GetParam(YandexUrlParams.AlbumId).Should().Be(expectedAlbumId);
        }

        [Theory]
        [InlineData("https://music.yandex.ru/album/ALBUM_ID/track/TRACK_ID", "ALBUM_ID", "TRACK_ID")]
        [InlineData("https://music.yandex.ru/album/ALBUM_ID/track/TRACK_ID/", "ALBUM_ID", "TRACK_ID")]
        public void ParsesTrackUrl(string url, string expectedAlbumId, string expectedTrackId)
        {
            var uri = new Uri(url);

            var result = _yandexUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YandexTrack);
            result.GetParam(YandexUrlParams.AlbumId).Should().Be(expectedAlbumId);
            result.GetParam(YandexUrlParams.TrackId).Should().Be(expectedTrackId);

        }

        private void AssertCommon(PlaylistUrlInfo playlistUrlInfo, Uri expectedUri, PlaylistType expectedPlaylistType)
        {
            playlistUrlInfo.Should().NotBeNull();
            playlistUrlInfo.Uri.Should().Be(expectedUri);
            playlistUrlInfo.Service.Should().Be(Service.YandexMusic);
            playlistUrlInfo.PlaylistType.Should().Be(expectedPlaylistType);
        }
    }
}

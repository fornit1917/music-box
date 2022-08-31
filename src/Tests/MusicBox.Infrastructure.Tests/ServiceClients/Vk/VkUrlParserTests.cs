using MusicBox.Domain;
using MusicBox.Domain.UrlParsing;
using MusicBox.Infrastructure.ServiceClients.Vk.UrlParsing;
using System;

namespace MusicBox.Infrastructure.Tests.ServiceClients.Vk
{
    public class VkUrlParserTests
    {
        private readonly VkUrlParser _vkUrlParser = new VkUrlParser();

        [Theory]
        [InlineData("https://vk.com/audios123", true)]
        [InlineData("https://m.vk.com/audios1234", true)]
        [InlineData("https://www.vk.com/audios123", true)]
        [InlineData("https://www.m.vk.com/audios1234", true)]
        [InlineData("https://unknownwebsite.com/audios1234", false)]
        public void ReturnsCanParseFlag(string url, bool expectedCanParse)
        {
            var uri = new Uri(url);
            _vkUrlParser.CanParseUrl(uri).Should().Be(expectedCanParse);
        }

        [Theory]
        [InlineData("https://vk.com/audios123", "123")]
        [InlineData("https://m.vk.com/audios1234", "1234")]
        [InlineData("https://vk.com/audios-123", "-123")]
        [InlineData("https://m.vk.com/audios-1234", "-1234")]
        public void ParsesAccountAllTracksUrl(string url, string expectedAccountId)
        {
            var uri = new Uri(url);
            PlaylistUrlInfo result = _vkUrlParser.ParseUrl(uri);

            AssertCommon(result, uri);
            result.PlaylistType.Should().Be(PlaylistType.VkAccountAudios);
            result.GetParam(VkUrlParams.AccountId).Should().Be(expectedAccountId);
        }

        [Theory]
        [InlineData("https://vk.com/id123?w=wall123_4567", "123_4567")]
        [InlineData("https://vk.com/wall123_4567", "123_4567")]
        [InlineData("https://m.vk.com/wall123_4567", "123_4567")]
        [InlineData("https://vk.com/publicname?w=wall-123_4567", "-123_4567")]
        [InlineData("https://vk.com/wall-123_4567", "-123_4567")]
        [InlineData("https://m.vk.com/wall-123_4567", "-123_4567")]
        public void ParsesWallPostUrl(string url, string expectedPostId)
        {
            var uri = new Uri(url);
            PlaylistUrlInfo result = _vkUrlParser.ParseUrl(uri);

            AssertCommon(result, uri);
            result.PlaylistType.Should().Be(PlaylistType.VkPost);
            result.GetParam(VkUrlParams.PostId).Should().Be(expectedPostId);
        }

        [Theory]
        [InlineData("https://vk.com/audios123?section=all&z=audio_playlist123_4567", "123_4567")]
        [InlineData("https://vk.com/music/playlist/123_4567", "123_4567")]
        [InlineData("https://m.vk.com/audio?act=audio_playlist123_4567&from=my_playlists&back_url=", "123_4567")]
        [InlineData("https://vk.com/audios123?z=audio_playlist-1234_567%2F8d019843cb392f9ec8", "-1234_567")]
        [InlineData("https://vk.com/music/playlist/-123_4567", "-123_4567")]
        [InlineData("https://m.vk.com/audios-123?from=audio_playlist-123_4567_8d019843cb392f9ec8", "-123_4567")]
        public void ParsesPlaylistUrl(string url, string expectedPlaylistId)
        {
            var uri = new Uri(url);
            PlaylistUrlInfo result = _vkUrlParser.ParseUrl(uri);

            AssertCommon(result, uri);
            result.PlaylistType.Should().Be(PlaylistType.VkPlaylist);
            result.GetParam(VkUrlParams.PlaylistId).Should().Be(expectedPlaylistId);
        }

        private void AssertCommon(PlaylistUrlInfo playlistUrlInfo, Uri uri)
        {
            playlistUrlInfo.Should().NotBeNull();
            playlistUrlInfo.Uri.Should().Be(uri);
            playlistUrlInfo.Service.Should().Be(Service.Vk);
        }
    }
}

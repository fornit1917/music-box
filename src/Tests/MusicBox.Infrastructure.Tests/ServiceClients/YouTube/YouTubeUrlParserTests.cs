using MusicBox.Domain;
using MusicBox.Domain.UrlParsing;
using MusicBox.Infrastructure.ServiceClients.YouTube.UrlParsing;
using System;

namespace MusicBox.Infrastructure.Tests.ServiceClients.YouTube
{
    public class YouTubeUrlParserTests
    {
        private readonly YouTubeUrlParser _ytUrlParser = new YouTubeUrlParser();

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=VIDEO_ID", true)]
        [InlineData("https://youtube.com/watch?v=VIDEO_ID&list=LIST_ID", true)]
        [InlineData("https://wrong-domain.com/watch?v=VIDEO_ID&list=LIST_ID", false)]
        public void ReturnsCanParseFlag(string url, bool expectedResult)
        {
            var uri = new Uri(url);
            _ytUrlParser.CanParseUrl(uri).Should().Be(expectedResult);
        }

        [Fact]
        public void ParsesVideoUrl()
        {
            var uri = new Uri("https://www.youtube.com/watch?v=VIDEO_ID");
            
            var result = _ytUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YouTubeVideo);
            result.GetParam(YouTubeUrlParams.VideoId).Should().Be("VIDEO_ID");
        }

        [Fact]
        public void ParsesPlaylistUrl()
        {
            var uri = new Uri("https://www.youtube.com/watch?v=VIDEO_ID&list=LIST_ID");

            var result = _ytUrlParser.ParseUrl(uri);

            AssertCommon(result, uri, PlaylistType.YouTubePlaylist);
            result.GetParam(YouTubeUrlParams.VideoId).Should().Be("VIDEO_ID");
            result.GetParam(YouTubeUrlParams.ListId).Should().Be("LIST_ID");
        }

        private void AssertCommon(PlaylistUrlInfo playlistUrlInfo, Uri uri, PlaylistType playlistType)
        {
            playlistUrlInfo.Should().NotBeNull();
            playlistUrlInfo.Uri.Should().Be(uri);
            playlistUrlInfo.Service.Should().Be(Service.YouTube);
            playlistUrlInfo.PlaylistType.Should().Be(playlistType);
        }
    }
}

using MusicBox.Domain.Exceptions;
using MusicBox.Domain.Models;
using MusicBox.Domain.Services;
using MusicBox.Domain.Services.Impl;

namespace MusicBox.Domain.Tests.Services
{
    public class PlaylistUrlParserTests
    {
        [Theory]
        [InlineData("https://test.com/path/to/something?q=123&a=2")]
        [InlineData("test.com/path/to/something")]
        public void ReturnsResultFromSuitableParser(string url)
        {
            var absoluteUri = url.IndexOf("://") > -1
                ? url
                : $"https://{url}";

            var expectedResult = new PlaylistUrlInfo(new Uri(absoluteUri), Service.YandexMusic, PlaylistType.YandexPlaylist, isAuthenticationRequired: true);
            var suitableParserMock = new Mock<IServiceUrlParser>();
            suitableParserMock
                .Setup(x => x.CanParseUrl(It.Is<Uri>(uri => uri.AbsoluteUri == absoluteUri)))
                .Returns(true);
            suitableParserMock
                .Setup(x => x.ParseUrl(It.Is<Uri>(uri => uri.AbsoluteUri == absoluteUri)))
                .Returns(expectedResult);

            var unsuitableParserMock = new Mock<IServiceUrlParser>();
            unsuitableParserMock.Setup(x => x.CanParseUrl(It.IsAny<Uri>())).Returns(false);

            var parser = new PlaylistUrlParser(new[] { unsuitableParserMock.Object, suitableParserMock.Object });
            var result = parser.ParseUrl(url);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void ThrowsExceptionIfUrlIsInvalid()
        {
            var invalidUrl = "aklsjdklasjdkljlkj";

            var parser = new PlaylistUrlParser(Array.Empty<IServiceUrlParser>());
            var act = () => parser.ParseUrl(invalidUrl);

            act.Should().Throw<PlaylistUrlParsingException>();
        }

        [Fact]
        public void ThrowsIfDoesNotHaveSuitableParser()
        {
            var url = "https://test.com/path";
            var unsuitableParserMock = new Mock<IServiceUrlParser>();
            unsuitableParserMock.Setup(x => x.CanParseUrl(It.IsAny<Uri>())).Returns(false);

            var parser = new PlaylistUrlParser(new[] { unsuitableParserMock.Object });
            var act = () => parser.ParseUrl(url);

            act.Should().Throw<PlaylistUrlParsingException>();
        }
    }
}

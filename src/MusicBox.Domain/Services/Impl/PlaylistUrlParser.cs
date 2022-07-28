using MusicBox.Domain.Exceptions;
using MusicBox.Domain.Models;

namespace MusicBox.Domain.Services.Impl
{
    public class PlaylistUrlParser : IPlaylistUrlParser
    {
        private IEnumerable<IServiceUrlParser> _parsers;

        public PlaylistUrlParser(IEnumerable<IServiceUrlParser> parsers)
        {
            _parsers = parsers;
        }

        public PlaylistUrlInfo ParseUrl(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch(UriFormatException e)
            {
                throw new PlaylistUrlParsingException("Incorrect url format.", e);
            }

            IServiceUrlParser? parser = _parsers.FirstOrDefault(p => p.CanParseUrl(uri));
            if (parser == null)
            {
                throw new PlaylistUrlParsingException($"Service {uri.Host} is not supported.");
            }

            return parser.ParseUrl(uri);
        }
    }
}

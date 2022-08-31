using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicBox.Domain.UrlParsing
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
            if (url.IndexOf("://") < 0)
            {
                url = "https://" + url;
            }

            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (UriFormatException e)
            {
                throw new PlaylistUrlParsingException("Incorrect url format.", e);
            }

            IServiceUrlParser? parser = _parsers.FirstOrDefault(p => p.CanParseUrl(uri));
            if (parser == null)
            {
                throw new PlaylistUrlParsingException($"Service '{uri.Host}' is not supported.");
            }

            return parser.ParseUrl(uri);
        }
    }
}

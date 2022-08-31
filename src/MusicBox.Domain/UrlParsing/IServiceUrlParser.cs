using System;

namespace MusicBox.Domain.UrlParsing
{
    public interface IServiceUrlParser
    {
        bool CanParseUrl(Uri uri);
        PlaylistUrlInfo ParseUrl(Uri uri);
    }
}

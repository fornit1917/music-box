using MusicBox.Domain.Models;

namespace MusicBox.Domain.Services
{
    public interface IServiceUrlParser
    {
        bool CanParseUrl(Uri uri);
        PlaylistUrlInfo ParseUrl(Uri uri);
    }
}

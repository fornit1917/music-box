using MusicBox.Domain.Models;

namespace MusicBox.Domain.Services
{
    public interface IPlaylistUrlParser
    {
        PlaylistUrlInfo ParseUrl(string url);
    }
}

using MusicBox.Domain.UrlParsing;

namespace MusicBox.Infrastructure
{
    public interface IAppServices
    {
        public IPlaylistUrlParser PlaylistUrlParser { get; }
    }
}

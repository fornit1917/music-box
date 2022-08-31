using MusicBox.Domain.Auth;
using MusicBox.Domain.UrlParsing;

namespace MusicBox.Infrastructure
{
    public interface IAppServices
    {
        public IPlaylistUrlParser PlaylistUrlParser { get; }
        public IAuthProcessFactory AuthProcessFactory { get; }
    }
}

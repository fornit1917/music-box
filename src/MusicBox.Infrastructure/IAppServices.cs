using MusicBox.Domain.Services;

namespace MusicBox.Infrastructure
{
    public interface IAppServices
    {
        public IPlaylistUrlParser PlaylistUrlParser { get; }
    }
}

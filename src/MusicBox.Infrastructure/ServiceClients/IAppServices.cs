using MusicBox.Domain.Services;

namespace MusicBox.Infrastructure.ServiceClients
{
    public interface IAppServices
    {
        public IPlaylistUrlParser PlaylistUrlParser { get; }
    }
}

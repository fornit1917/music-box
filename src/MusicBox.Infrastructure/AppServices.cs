using MusicBox.Domain.Services;
using MusicBox.Domain.Services.Impl;
using MusicBox.Infrastructure.ServiceClients.Vk;
using MusicBox.Infrastructure.ServiceClients.YandexMusic;
using MusicBox.Infrastructure.ServiceClients.YouTube;

namespace MusicBox.Infrastructure
{
    public class AppServices : IAppServices
    {
        public IPlaylistUrlParser PlaylistUrlParser { get; private set; }

        protected AppServices()
        {
        }

        public static IAppServices Create()
        {
            var appServices = new AppServices();
            appServices.PlaylistUrlParser = CreatePlaylistUrlParser();
            return appServices;
        }

        private static IPlaylistUrlParser CreatePlaylistUrlParser()
        {
            IServiceUrlParser[] urlParsers = new IServiceUrlParser[]
            {
                new VkUrlParser(),
                new YandexUrlParser(),
                new YouTubeUrlParser()
            };
            return new PlaylistUrlParser(urlParsers);
        }
    }
}

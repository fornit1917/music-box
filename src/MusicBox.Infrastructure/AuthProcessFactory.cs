using MusicBox.Domain;
using MusicBox.Domain.Auth;
using MusicBox.Infrastructure.Networking;
using MusicBox.Infrastructure.ServiceClients.YandexMusic.Auth;
using System;

namespace MusicBox.Infrastructure
{
    public class AuthProcessFactory : IAuthProcessFactory
    {
        private readonly IWebClientFactory _httpClientFactory;

        public AuthProcessFactory(IWebClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IAuthProcess CreateAuthProcess(Service service)
        {
            return service switch
            {
                Service.YandexMusic => new YandexAuthProcess(_httpClientFactory.GetHttpClient(service)),
                _ => throw new NotImplementedException($"Authentication for service {service} is not implemented yet"),
            };
        }
    }
}

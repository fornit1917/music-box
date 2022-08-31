using MusicBox.Domain;
using System.Collections.Concurrent;
using System.Net.Http;

namespace MusicBox.Infrastructure.Networking
{
    public class WebClientFactory : IWebClientFactory
    {
        private readonly ConcurrentDictionary<Service, IWebClient> _clients;

        public WebClientFactory()
        {
            _clients = new ConcurrentDictionary<Service, IWebClient>();
        }

        public IWebClient GetHttpClient(Service service)
        {
            return _clients.GetOrAdd(service, CreateHttpClient);
        }

        private IWebClient CreateHttpClient(Service service)
        {
            var webClient = new WebClient();

            var httpClient = webClient.HttpClient;
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36"); 
            
            return webClient;
        }
    }
}

using System.Net;
using System.Net.Http;

namespace MusicBox.Infrastructure.Networking
{
    public class WebClient : IWebClient
    {
        public HttpClient HttpClient { get; }

        public CookieContainer CookieContainer { get; }

        public WebClient()
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            HttpClient = new HttpClient(handler);
            CookieContainer = cookies;
        }
    }
}

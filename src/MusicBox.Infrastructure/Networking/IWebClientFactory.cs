using MusicBox.Domain;
using System.Net.Http;

namespace MusicBox.Infrastructure.Networking
{
    public interface IWebClientFactory
    {
        IWebClient GetHttpClient(Service service);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicBox.Infrastructure.Networking
{
    public interface IWebClient
    {
        HttpClient HttpClient { get; }
        CookieContainer CookieContainer { get; }
    }
}

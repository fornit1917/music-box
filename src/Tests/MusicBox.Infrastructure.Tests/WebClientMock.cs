using Moq;
using MusicBox.Infrastructure.Networking;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicBox.Infrastructure.Tests
{
    public class WebClientMock
    {
        public Mock<IWebClient> Mock { get; }
        public MockHttpMessageHandler MockHttp { get; }
        public CookieContainer Cookies { get; }

        public IWebClient Object => Mock.Object;

        public WebClientMock()
        {
            Cookies = new CookieContainer();
            MockHttp = new MockHttpMessageHandler();
            Mock = new Mock<IWebClient>();
            Mock.Setup(x => x.HttpClient).Returns(MockHttp.ToHttpClient());
            Mock.Setup(x => x.CookieContainer).Returns(Cookies);
        }
    }
}

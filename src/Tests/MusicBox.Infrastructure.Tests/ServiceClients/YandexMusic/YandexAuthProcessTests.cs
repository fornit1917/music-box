using MusicBox.Domain.Auth;
using MusicBox.Infrastructure.ServiceClients.YandexMusic.Auth;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicBox.Infrastructure.Tests.ServiceClients.YandexMusic
{
    public class YandexAuthProcessTests
    {
        private readonly WebClientMock _webClientMock;

        private readonly YandexAuthProcess _authProcess;

        public YandexAuthProcessTests()
        {
            _webClientMock = new WebClientMock();
            _authProcess = new YandexAuthProcess(_webClientMock.Object);
        }

        [Fact]
        public async Task ReturnsCookiesFromResponseIfCredentialsValid()
        {
            string username = "username";
            string password = "password";

            _webClientMock.MockHttp
                .When(HttpMethod.Post, "https://passport.yandex.ru/auth*")
                .WithFormData(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("login", username),
                    new KeyValuePair<string, string>("passwd", password),
                })
                .Respond(HttpStatusCode.OK);

            var cookie1 = new Cookie("cookie1", "value1", "", "music.yandex.ru");
            var cookie2 = new Cookie("cookie2", "value2", "", "music.yandex.ru");
            _webClientMock.Cookies.Add(cookie1);
            _webClientMock.Cookies.Add(cookie2);

            var authResult = await _authProcess.SendCredentials(username, password);

            var expectedData = new Dictionary<string, string>
            {
                { cookie1.Name, cookie1.Value },
                { cookie2.Name, cookie2.Value },
            };

            authResult.Data.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public async Task ThrowsIfNotSuccessResponse()
        {
            _webClientMock.MockHttp
                .When(HttpMethod.Post, "https://passport.yandex.ru/auth*")
                .Respond(HttpStatusCode.InternalServerError);

            var act = () => _authProcess.SendCredentials("username", "password");

            await act.Should().ThrowAsync<AuthException>();
        }
    }
}

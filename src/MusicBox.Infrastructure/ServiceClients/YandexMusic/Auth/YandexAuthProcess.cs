using MusicBox.Domain.Auth;
using MusicBox.Infrastructure.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicBox.Infrastructure.ServiceClients.YandexMusic.Auth
{
    public class YandexAuthProcess : IAuthProcess
    {
        private const string LoginFormUrl = "https://passport.yandex.ru/auth?origin=music_button-header";
        private const string UserNameParam = "login";
        private const string PasswordParam = "passwd";


        private readonly IWebClient _webClient;

        public YandexAuthProcess(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<AuthResult> SendCredentials(string userName, string password)
        {
            var data = new[]
            {
                new KeyValuePair<string, string>(UserNameParam, userName),
                new KeyValuePair<string, string>(PasswordParam, password),
            };

            var response = await _webClient.HttpClient.PostAsync(LoginFormUrl, new FormUrlEncodedContent(data));

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthException($"Yandex authentication request returned not success response: {response.ReasonPhrase}");
            }

            // todo: handle case with incorrect credentials

            var cookies = _webClient.CookieContainer
                .GetCookies(new Uri("https://music.yandex.ru"))
                .AsEnumerable()
                .ToDictionary(x => x.Name, x => x.Value);
            
            return new AuthResult(cookies);
        }
    }
}

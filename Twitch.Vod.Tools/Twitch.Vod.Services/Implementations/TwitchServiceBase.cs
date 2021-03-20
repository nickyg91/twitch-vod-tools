using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Models;
using System.Text.Json;

namespace Twitch.Vod.Services.Implementations
{
    public abstract class TwitchServiceBase
    {
        private readonly TwitchConfigurationSection _twitchConfig;
        private Token _token;
        protected TwitchServiceBase(TwitchConfigurationSection config)
        {
            _twitchConfig = config;
        }

        protected async Task<T> CallTwitchApi<T>(string url, object jsonPayload, HttpVerb httpVerb, string userToken)
        {
            if (_token == null)
            {
                await GetAccessToken(userToken);
            }

            if (_token.IsExpired)
            {
                await GetTokenByRefreshToken();
            }
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("client-id", _twitchConfig.ClientId);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);
            //meed to check for pagination key if any
            HttpResponseMessage response = null;

            switch (httpVerb)
            {
                case HttpVerb.POST:
                    var payload = "";
                    if (jsonPayload != null)
                    {
                        payload = JsonSerializer.Serialize(jsonPayload);
                        response = await httpClient.PostAsJsonAsync(url, payload);
                    }
                    else
                    {
                        response = await httpClient.PostAsync(url, new StringContent(payload));
                    }
                    break;
                case HttpVerb.GET:
                    response = await httpClient.GetAsync(url);
                    break;
                case HttpVerb.PUT:
                    break;
                case HttpVerb.DELETE:
                    break;
                case HttpVerb.PATCH:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(httpVerb), httpVerb, null);
            }

            if (response == null) throw new Exception("Http response was null.");

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(content);

            return JsonSerializer.Deserialize<T>(content);
        }

        private async Task GetAccessToken(string userToken)
        {
            using var httpClient = new HttpClient();
            var url = $"{_twitchConfig.RedirectUrl}?client_id={_twitchConfig.ClientId}&client_secret={_twitchConfig.ClientSecret}&code={userToken}&grant_type=authorization_code&redirect_uri={_twitchConfig.RedirectUrl}";
            var response = await httpClient.PostAsync(url, new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(content);
            _token = JsonSerializer.Deserialize<Token>(content);
        }

        private async Task GetTokenByRefreshToken()
        {
            using var httpClient = new HttpClient();
            var url = $"{_twitchConfig.RedirectUrl}?client_id={_twitchConfig.ClientId}&client_secret={_twitchConfig.ClientSecret}&refresh_token={_token.RefreshToken}&grant_type=refresh&redirect_uri={_twitchConfig.RedirectUrl}";
            var response = await httpClient.PostAsync(url, new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(content);
            _token = JsonSerializer.Deserialize<Token>(content);
        }
    }
}

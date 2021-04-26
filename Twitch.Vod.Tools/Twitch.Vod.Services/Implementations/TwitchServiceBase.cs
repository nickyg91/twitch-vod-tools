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
        protected readonly TwitchConfigurationSection TwitchConfig;
        private Token _token;
        protected TwitchServiceBase(TwitchConfigurationSection config)
        {
            TwitchConfig = config;
        }

        protected async Task<T> CallTwitchApi<T>(string url, object jsonPayload, HttpVerb httpVerb)
        {
            if (_token == null)
            {
                await GetAccessToken();
            }

            if (_token.IsExpired)
            {
                await GetTokenByRefreshToken();
            }
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("client-id", TwitchConfig.ClientId);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);
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
                    response = await httpClient.DeleteAsync(url);
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

        private async Task GetAccessToken()
        {
            using var httpClient = new HttpClient();
            var url = $"{TwitchConfig.TwitchTokenBaseUrl}?client_id={TwitchConfig.ClientId}&client_secret={TwitchConfig.ClientSecret}&grant_type=client_credentials";
            var response = await httpClient.PostAsync(url, new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(content);
            _token = JsonSerializer.Deserialize<Token>(content);
        }

        private async Task GetTokenByRefreshToken()
        {
            using var httpClient = new HttpClient();
            var url = $"{TwitchConfig.TwitchTokenBaseUrl}?client_id={TwitchConfig.ClientId}&client_secret={TwitchConfig.ClientSecret}&refresh_token={_token.RefreshToken}&grant_type=refresh";
            var response = await httpClient.PostAsync(url, new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(content);
            _token = JsonSerializer.Deserialize<Token>(content);
        }
    }
}

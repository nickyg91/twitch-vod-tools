using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<T> GetTwitchApi<T>(string url)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("client-id", _twitchConfig.ClientId);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);
            var response = await httpClient.GetAsync(url);
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

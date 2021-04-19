using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;

namespace Twitch.Vod.Services.Implementations
{
    public class TwitchUserService : TwitchServiceBase, ITwitchUserService
    {
        public TwitchUserService(TwitchConfigurationSection config) : base(config)
        {
        }

        //public async Task<TwitchResponse<List<TwitchUser>>> GetTwitchUser()
        //{
        //    return await CallTwitchApi<TwitchResponse<List<TwitchUser>>>("https://api.twitch.tv/helix/users", null, HttpVerb.GET,);
        //}

        //public async Task<Token> GetOauthToken(string userToken)
        //{
        //    using var httpClient = new HttpClient();
        //    var url = $"{TwitchConfig.TwitchTokenBaseUrl}?client_id={TwitchConfig.ClientId}&client_secret={TwitchConfig.ClientSecret}&code={userToken}&grant_type=authorization_code&redirect_uri={TwitchConfig.RedirectUrl}";
        //    var response = await httpClient.PostAsync(url, new StringContent(""));
        //    var content = await response.Content.ReadAsStringAsync();
        //    if (!response.IsSuccessStatusCode) throw new Exception(content);
        //    return JsonSerializer.Deserialize<Token>(content);
        //}

        //public async Task<Token> RefreshOauthToken(string userToken, string refreshToken)
        //{
        //    using var httpClient = new HttpClient();
        //    var url = $"{TwitchConfig.TwitchTokenBaseUrl}?client_id={TwitchConfig.ClientId}&client_secret={TwitchConfig.ClientSecret}&refresh_token={refreshToken}&grant_type=refresh&redirect_uri={TwitchConfig.RedirectUrl}";
        //    var response = await httpClient.PostAsync(url, new StringContent(""));
        //    var content = await response.Content.ReadAsStringAsync();
        //    if (!response.IsSuccessStatusCode) throw new Exception(content);
        //    return JsonSerializer.Deserialize<Token>(content);
        //}
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Implementations
{
    public class TwitchUserService : TwitchServiceBase, ITwitchUserService
    {
        public TwitchUserService(TwitchConfigurationSection config) : base(config)
        {
        }

        public async Task<TwitchResponse<List<TwitchUser>>> GetTwitchUser(string userToken)
        {
            return await CallTwitchApi<TwitchResponse<List<TwitchUser>>>("https://api.twitch.tv/helix/users", null, HttpVerb.GET, userToken);
        }
    }
}

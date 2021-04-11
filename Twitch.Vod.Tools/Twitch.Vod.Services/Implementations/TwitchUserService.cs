using System;
using System.Threading.Tasks;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models;
using Twitch.Vod.Services.Models.Dtos;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Implementations
{
    public class TwitchUserService : TwitchServiceBase, ITwitchUserService
    {
        public TwitchUserService(TwitchConfigurationSection config) : base(config)
        {
        }

        public async Task<TwitchUser> GetTwitchUser(string userToken)
        {
            return await CallTwitchApi<TwitchUser>("https://api.twitch.tv/helix/user", null, HttpVerb.GET, userToken);
        }
    }
}

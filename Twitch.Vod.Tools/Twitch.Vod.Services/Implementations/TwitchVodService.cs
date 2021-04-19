using System.Collections.Generic;
using System.Threading.Tasks;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Implementations
{
    public class TwitchVodService : TwitchServiceBase, ITwitchVodService
    {
        public TwitchVodService(TwitchConfigurationSection config) : base(config)
        {
        }

        public Task<TwitchResponse<List<TwitchVod>>> GetTwitchVods(string userId, string cursor)
        {
            var url = string.IsNullOrEmpty(cursor) ? $"https://api.twitch.tv/helix/videos?user_id={userId}" : $"https://api.twitch.tv/helix/videos?user_id={userId}&after={cursor}";
            return CallTwitchApi<TwitchResponse<List<TwitchVod>>>(url, null,
                HttpVerb.GET);
        }
    }
}

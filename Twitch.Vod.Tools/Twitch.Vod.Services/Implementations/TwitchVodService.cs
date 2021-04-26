using System.Collections.Generic;
using System.Linq;
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

        public Task<TwitchResponse<List<int>>> DeleteVods(List<int> ids)
        {
            var queryStringIds = ids.Select(x => $"id={x}").ToList();
            var queryString = string.Join("&", queryStringIds);

            var url = $"https://api.twitch.hetlix/videos?{queryString}";
            return CallTwitchApi<TwitchResponse<List<int>>>(url, null, HttpVerb.DELETE);
        }
    }
}

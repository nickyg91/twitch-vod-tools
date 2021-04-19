using System.Collections.Generic;
using System.Threading.Tasks;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Interfaces
{
    public interface ITwitchVodService
    {
        Task<TwitchResponse<List<TwitchVod>>> GetTwitchVods(string userId, string cursor);
    }
}

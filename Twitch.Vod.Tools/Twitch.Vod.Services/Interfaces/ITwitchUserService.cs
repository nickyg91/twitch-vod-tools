using System.Collections.Generic;
using System.Threading.Tasks;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Interfaces
{
    public interface ITwitchUserService
    {
        Task<TwitchResponse<List<TwitchUser>>> GetTwitchUser(string userToken);
    }
}

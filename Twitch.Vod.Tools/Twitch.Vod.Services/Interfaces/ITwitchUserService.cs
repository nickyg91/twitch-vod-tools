using System.Threading.Tasks;
using Twitch.Vod.Services.Models.Dtos;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Interfaces
{
    public interface ITwitchUserService
    {
        Task<TwitchUser> GetTwitchUser(string userToken);
    }
}

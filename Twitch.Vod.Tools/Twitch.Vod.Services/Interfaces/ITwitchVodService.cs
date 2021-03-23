using System.Threading.Tasks;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Interfaces
{
    public interface ITwitchVodService
    {
        Task<TwitchVodContainer> GetTwitchVods(string userId, string userToken, string cursor);
    }
}

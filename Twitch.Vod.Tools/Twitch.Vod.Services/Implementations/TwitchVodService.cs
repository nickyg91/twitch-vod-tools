using System.Threading.Tasks;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Implementations
{
    public class TwitchVodService : TwitchServiceBase, ITwitchVodService
    {
        public TwitchVodService(TwitchConfigurationSection config) : base(config)
        {
        }

        public Task<TwitchVodContainer> GetTwitchVods()
        {
            throw new System.NotImplementedException();
        }

        public Task<TwitchVodContainer> GetTwitchVodsWithPagination(string cursor)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitch.Vod.Services.Models.Twitch;

namespace Twitch.Vod.Services.Interfaces
{
    public interface ITwitchVodService
    {
        Task<TwitchVodContainer> GetTwitchVods();
        Task<TwitchVodContainer> GetTwitchVodsWithPagination(string cursor);
    }
}

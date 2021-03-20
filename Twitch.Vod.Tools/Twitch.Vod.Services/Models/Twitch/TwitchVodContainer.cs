using System.Collections.Generic;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchVodContainer
    {
        public List<TwitchVod> Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}

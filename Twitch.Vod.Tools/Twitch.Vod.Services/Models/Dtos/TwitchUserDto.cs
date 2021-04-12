using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Dtos
{
    public class TwitchUserDto : ITwitchUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImageUrl { get; set; }
        public long ViewCount { get; set; }
    }
}

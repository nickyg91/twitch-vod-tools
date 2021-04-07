using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Dtos
{
    public class TwitchUserDto : ITwitchUser
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }
}

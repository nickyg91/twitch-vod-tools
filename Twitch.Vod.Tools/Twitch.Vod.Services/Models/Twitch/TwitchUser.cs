
using System.Text.Json.Serialization;
using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchUser: ITwitchUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        [JsonPropertyName("profile_image_url")]
        public string ProfileImageUrl { get; set; }
        [JsonPropertyName("view_count")]
        public long ViewCount { get; set; }

    }
}

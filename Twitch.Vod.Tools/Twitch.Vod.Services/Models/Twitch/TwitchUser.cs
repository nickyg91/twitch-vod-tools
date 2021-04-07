
using System.Text.Json.Serialization;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchUser
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }
}

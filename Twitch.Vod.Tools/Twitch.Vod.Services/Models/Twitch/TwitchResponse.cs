using System.Text.Json.Serialization;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class Pagination
    {
        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }
    }
}

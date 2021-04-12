using System;
using System.Text.Json.Serialization;
using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchVod : ITwitchVod
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("user_name")]
        public string Username { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
        [JsonPropertyName("view_count")]
        public long ViewCount { get; set; }
        [JsonPropertyName("viewable")]
        public string Viewable { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
    }
}

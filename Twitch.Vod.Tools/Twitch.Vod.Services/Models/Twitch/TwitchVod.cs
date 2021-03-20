using System;
using System.Text.Json.Serialization;
using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Twitch
{
    public class TwitchVod : ITwitchVod
    {
        public long Id { get; set; }
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        [JsonPropertyName("user_name")]
        public string Username { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
        [JsonPropertyName("view_count")]
        public int ViewCount { get; set; }
        public string Viewable { get; set; }
        public string Duration { get; set; }
    }
}

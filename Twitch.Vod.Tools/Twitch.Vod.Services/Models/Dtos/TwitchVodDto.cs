using System;
using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Dtos
{
    public class TwitchVodDto : ITwitchVod
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public int ViewCount { get; set; }
        public string Viewable { get; set; }
        public string Duration { get; set; }
    }
}

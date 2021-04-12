using System;
using Twitch.Vod.Services.Models.Interfaces;

namespace Twitch.Vod.Services.Models.Dtos
{
    public class TwitchVodDto : ITwitchVod
    {
        public TwitchVodDto() { }

        public TwitchVodDto(ITwitchVod vod)
        {
            Id = vod.Id;
            UserId = vod.UserId;
            Username = vod.Username;
            Description = vod.Description;
            Title = vod.Title;
            CreatedAt = vod.CreatedAt;
            PublishedAt = vod.PublishedAt;
            Url = vod.Url;
            ThumbnailUrl = vod.ThumbnailUrl.Replace("%{width}", "460").Replace("%{height}", "460");
            ViewCount = vod.ViewCount;
            Viewable = vod.Viewable;
            Duration = vod.Duration;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public long ViewCount { get; set; }
        public string Viewable { get; set; }
        public string Duration { get; set; }
    }
}

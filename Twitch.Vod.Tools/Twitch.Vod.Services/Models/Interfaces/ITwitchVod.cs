using System;

namespace Twitch.Vod.Services.Models.Interfaces
{
    public interface ITwitchVod
    {
        string Id { get; set; }
        string UserId { get; set; }
        string Username { get; set; }
        string Description { get; set; }
        string Title { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime PublishedAt { get; set; }
        string Url { get; set; }
        string ThumbnailUrl { get; set; }
        long ViewCount { get; set; }
        string Viewable { get; set; }
        string Duration { get; set; }
    }
}

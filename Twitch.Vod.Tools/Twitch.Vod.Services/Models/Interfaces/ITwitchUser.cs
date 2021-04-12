namespace Twitch.Vod.Services.Models.Interfaces
{
    public interface ITwitchUser
    {
        string Id { get; set; }
        string DisplayName { get; set; }
        string ProfileImageUrl { get; set; }
        long ViewCount { get; set; }
    }
}

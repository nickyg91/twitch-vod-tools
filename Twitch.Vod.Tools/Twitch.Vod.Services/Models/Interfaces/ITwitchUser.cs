namespace Twitch.Vod.Services.Models.Interfaces
{
    public interface ITwitchUser
    {
        int Id { get; set; }
        string DisplayName { get; set; }
    }
}

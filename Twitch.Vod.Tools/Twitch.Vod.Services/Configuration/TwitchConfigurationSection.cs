namespace Twitch.Vod.Services.Configuration
{
    public class TwitchConfigurationSection
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string TwitchTokenBaseUrl { get; set; }
    }
}

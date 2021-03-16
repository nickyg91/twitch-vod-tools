using System.Security;
using System.Text.Json.Serialization;

namespace Twitch.Vod.Tools.Configuration
{
    public class TwitchConfigurationSection
    {
        [JsonPropertyName("client_id")]
        public SecureString ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public SecureString ClientSecret { get; set; }
        [JsonPropertyName("redirect_url")]
        public SecureString RedirectUrl { get; set; }
    }
}

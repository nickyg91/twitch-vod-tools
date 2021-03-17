using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Twitch.Vod.Services.Models
{
    public class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public List<string> Scopes { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        public bool IsExpired
        {
            get
            {
                var expirationDate = TimeSpan.FromSeconds(ExpiresIn);
                return DateTime.Now.TimeOfDay > expirationDate;
            }
        }
    }
}

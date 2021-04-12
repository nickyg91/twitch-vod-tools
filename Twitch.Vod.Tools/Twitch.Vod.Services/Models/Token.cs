using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Twitch.Vod.Services.Models
{
    public class Token
    {
        private readonly DateTime _dateCreated = DateTime.UtcNow;

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

        public DateTime ExpirationDate => _dateCreated.AddSeconds(ExpiresIn);

        public bool IsExpired => DateTime.UtcNow > ExpirationDate;
    }
}

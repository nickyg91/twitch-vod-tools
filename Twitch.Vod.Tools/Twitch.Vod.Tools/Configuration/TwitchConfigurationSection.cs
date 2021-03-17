﻿using System.Security;

namespace Twitch.Vod.Tools.Configuration
{
    public class TwitchConfigurationSection
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
    }
}

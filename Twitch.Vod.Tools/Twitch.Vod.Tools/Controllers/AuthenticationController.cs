using Microsoft.AspNetCore.Mvc;
using Twitch.Vod.Tools.Configuration;

namespace Twitch.Vod.Tools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TwitchConfigurationSection _twitchConfig;
        public AuthenticationController(TwitchConfigurationSection twitchConfiguration)
        {
            _twitchConfig = twitchConfiguration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = _twitchConfig.RedirectUrl;
            return Ok();
        }
    }
}

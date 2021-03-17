using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twitch.Vod.Services.Configuration;

namespace Twitch.Vod.Tools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TwitchConfigurationSection _twitchConfig;
        public AuthenticationController(IOptions<TwitchConfigurationSection> twitchConfiguration)
        {
            _twitchConfig = twitchConfiguration.Value;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = _twitchConfig.RedirectUrl;
            return Ok();
        }

        [HttpGet("config")]
        public IActionResult Config()
        {
            return Ok(
                new
                {
                    _twitchConfig.ClientId,
                    _twitchConfig.RedirectUrl
                });
        }
    }
}

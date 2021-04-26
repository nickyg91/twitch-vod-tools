using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models.Dtos;

namespace Twitch.Vod.Tools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TwitchConfigurationSection _twitchConfig;
        private readonly ITwitchUserService _userService;
        public AuthenticationController(IOptions<TwitchConfigurationSection> twitchConfiguration, ITwitchUserService userService)
        {
            _twitchConfig = twitchConfiguration.Value;
            _userService = userService;
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

        [HttpGet("user"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userDto = new TwitchUserDto
            {
                DisplayName = User.Identity?.Name,
                Id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                ProfileImageUrl = User.Claims.FirstOrDefault(x => x.Type == TwitchAuthenticationConstants.Claims.ProfileImageUrl)?.Value,
            };
            return Ok(userDto);
        }

        [HttpGet("login")]
        public IActionResult Authenticate()
        {
            var challenge = Challenge(new AuthenticationProperties
            {
                RedirectUri = _twitchConfig.RedirectUrl
            }, TwitchAuthenticationDefaults.AuthenticationScheme);
            return challenge;
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            return SignOut(new AuthenticationProperties
            {
            }, TwitchAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

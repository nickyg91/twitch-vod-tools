using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var token = HttpContext.Request.Headers["x-user-access-token"];
            var user = await _userService.GetTwitchUser(token);
            var userDto = new TwitchUserDto
            {
                DisplayName = user.Data.FirstOrDefault()?.DisplayName,
                Id = user.Data.FirstOrDefault()?.Id,
                ProfileImageUrl = user.Data.FirstOrDefault()?.ProfileImageUrl,
                ViewCount = user.Data.FirstOrDefault()?.ViewCount ?? 0
            };
            return Ok(userDto);
        }
    }
}

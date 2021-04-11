using System.Threading.Tasks;
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
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var user = await _userService.GetTwitchUser(token);
            var userDto = new TwitchUserDto
            {
                DisplayName = user.DisplayName,
                Id = user.Id
            };
            return Ok(userDto);
        }
    }
}

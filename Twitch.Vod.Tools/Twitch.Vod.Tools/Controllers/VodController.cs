using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models.Dtos;

namespace Twitch.Vod.Tools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VodController : ControllerBase
    {
        private readonly ITwitchVodService _vodService;
        public VodController(ITwitchVodService vodService)
        {
            _vodService = vodService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetVods(string userId, string pagination = null)
        {
            var token = HttpContext.Request.Headers["x-user-access-token"];
            var vods = await _vodService.GetTwitchVods(userId, token, pagination);
            var dtos = vods.Data.Select(x => new TwitchVodDto(x));
            return Ok(new
            {
                vods = dtos,
                pagination = vods.Pagination
            });
        }
    }
}

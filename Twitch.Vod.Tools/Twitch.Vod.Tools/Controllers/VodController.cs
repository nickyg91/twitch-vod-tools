using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models.Dtos;

namespace Twitch.Vod.Tools.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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
            var vods = await _vodService.GetTwitchVods(userId,  pagination);
            var dtos = vods.Data.Select(x => new TwitchVodDto(x));
            return Ok(new
            {
                vods = dtos,
                pagination = vods.Pagination
            });
        }
    }
}

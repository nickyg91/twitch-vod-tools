using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitch.Vod.Services.Interfaces;

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
            var token = HttpContext.Request.Headers["Authorization"];
            var vods = await _vodService.GetTwitchVods(userId, token, pagination);
            
            return Ok(vods);
        }
    }
}

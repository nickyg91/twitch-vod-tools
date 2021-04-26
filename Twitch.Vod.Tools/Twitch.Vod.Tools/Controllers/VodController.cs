using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitch.Vod.Services.Interfaces;
using Twitch.Vod.Services.Models.Dtos;
using Twitch.Vod.Services.Models.Twitch;

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
        public async Task<IActionResult> GetVods(string userId, string pagination = null, bool getAll = false)
        {
            var vods = await _vodService.GetTwitchVods(userId,  pagination);
            if (getAll)
            {
                var allVods = new List<TwitchVod>();
                allVods.AddRange(vods.Data);
                var cursor = vods.Pagination.Cursor;
                while (!string.IsNullOrEmpty(cursor))
                {
                    vods = await _vodService.GetTwitchVods(userId, vods.Pagination.Cursor);
                    cursor = vods.Pagination.Cursor;
                    allVods.AddRange(vods.Data);
                }

                var allVodsDtos = allVods.Select(x => new TwitchVodDto(x)).Distinct();
                return Ok(new
                {
                    vods = allVodsDtos
                });
            }

            var dtos = vods.Data.Select(x => new TwitchVodDto(x));
            return Ok(new
            {
                vods = dtos,
                pagination = vods.Pagination
            });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteVods(List<int> ids)
        {
            var deletedItems = new List<int>();
            var skipped = 0;
            // twitch only allows you to delete five at a time.
            while (ids.Any())
            {
                var five = ids.Take(Math.Min(5, ids.Count)).ToList();
                var deleted = await _vodService.DeleteVods(five);
                ids.RemoveRange(0, Math.Min(5, ids.Count));
                deletedItems.AddRange(deleted.Data);
            }

            return Ok(deletedItems);
        }
    }
}

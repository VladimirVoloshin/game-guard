using GameGuard.Application.Players.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameGuard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<PlayersSummaryDto> GetPlayersSummary()
        {
            var playersStats = new PlayersSummaryDto(5, 4, 2, 1);

            return Ok(playersStats);
        }
    }
}

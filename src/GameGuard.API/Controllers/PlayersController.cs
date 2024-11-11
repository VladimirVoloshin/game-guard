using GameGuard.Application.Players;
using GameGuard.Application.Players.Dtos;
using GameGuard.Domain.Players;
using Microsoft.AspNetCore.Mvc;

namespace GameGuard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PlayersSummaryDto>> GetPlayersSummaryAsync()
        {
            var playersStats = await _playerService.GetPlayersSummaryAsync();

            return Ok(playersStats);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersAsync()
        {
            var players = await _playerService.GetPlayersAsync();

            return Ok(players);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePlayerStatus(
            int id,
            [FromBody] UpdatePlayerStatusDto request
        )
        {
            await _playerService.UpdatePlayerStatusAsync(id, (PlayerStatusType)request.StatusId);
            return NoContent();
        }
    }
}

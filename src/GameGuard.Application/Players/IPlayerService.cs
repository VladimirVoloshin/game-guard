using GameGuard.Application.Players.Dtos;

namespace GameGuard.Application.Players
{
    public interface IPlayerService
    {
        Task<PlayersSummaryDto> GetPlayersSummaryAsync();
        Task<IEnumerable<PlayerDto>> GetPlayersAsync();
    }
}

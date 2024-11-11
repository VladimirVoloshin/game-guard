using GameGuard.Application.Players.Dtos;
using GameGuard.Domain.Players;

namespace GameGuard.Application.Players
{
    public interface IPlayerService
    {
        Task<PlayersSummaryDto> GetPlayersSummaryAsync();
        Task<IEnumerable<PlayerDto>> GetPlayersAsync();
        Task UpdatePlayerStatusAsync(int id, PlayerStatusType newStatus);
    }
}

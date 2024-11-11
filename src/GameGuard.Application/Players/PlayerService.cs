using GameGuard.Application.Players.Dtos;
using GameGuard.Domain.Players;

namespace GameGuard.Application.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayersSummaryDto> GetPlayersSummaryAsync()
        {
            var statusSummary = await _playerRepository.GetPlayersStatusesSummaryAsync();

            int total = statusSummary.Sum(s => s.Count);
            int active =
                statusSummary.FirstOrDefault(s => s.Status == PlayerStatusType.Active)?.Count ?? 0;
            int suspicious =
                statusSummary.FirstOrDefault(s => s.Status == PlayerStatusType.Suspicious)?.Count
                ?? 0;
            int banned =
                statusSummary.FirstOrDefault(s => s.Status == PlayerStatusType.Banned)?.Count ?? 0;

            return new PlayersSummaryDto(
                Total: total,
                Active: active,
                Suspicious: suspicious,
                Banned: banned
            );
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();

            return players.Select(x => new PlayerDto(
                x.Id,
                x.Username,
                (int)x.Status,
                x.Status.ToString()
            ));
        }
    }
}

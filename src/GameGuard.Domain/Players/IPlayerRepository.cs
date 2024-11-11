namespace GameGuard.Domain.Players
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<PlayerStatusSummary>> GetPlayersStatusesSummaryAsync();
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player?> GetByIdAsync(int playerId);
        Task UpdateAsync(Player player);
    }
}

namespace GameGuard.Domain.Players
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<PlayerStatusSummary>> GetPlayersStatusesSummaryAsync();
        Task<IEnumerable<Player>> GetAllAsync();
    }
}

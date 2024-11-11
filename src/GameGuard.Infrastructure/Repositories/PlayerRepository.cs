using GameGuard.Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace GameGuard.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayerStatusSummary>> GetPlayersStatusesSummaryAsync()
        {
            return await _context
                .Players.GroupBy(p => p.Status)
                .Select(g => new PlayerStatusSummary(g.Key, g.Count()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }
    }
}

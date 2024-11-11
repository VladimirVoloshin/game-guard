using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace GameGuard.Infrastructure.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly AppDbContext _context;

        public ActivityLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<ActivityLog> Activities, int TotalCount)> GetAllAsync(
            ISpecification<ActivityLog> specification,
            int page,
            int pageSize
        )
        {
            var query = _context
                .ActivityLogs.Include(x => x.Player)
                .Where(specification.ToExpression());

            int totalCount = await query.CountAsync();

            var activities = await query
                .OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (activities, totalCount);
        }

        public async Task AddAsync(ActivityLog activity)
        {
            await _context.ActivityLogs.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<ActivityLog?> GetByIdAsync(int activityId)
        {
            return await _context.ActivityLogs.FindAsync(activityId);
        }

        public async Task UpdateAsync(ActivityLog activity)
        {
            _context.ActivityLogs.Attach(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<ActivityLog>> GetRecentActivityLogsAsync(
            int playerId,
            int numberLatestActivities,
            ActivityActionType? activityActionType = null
        )
        {
            return await _context
                .ActivityLogs.Where(x =>
                    x.PlayerId == playerId
                    && (activityActionType == null || x.Action == activityActionType)
                )
                .OrderByDescending(x => x.Timestamp)
                .Take(numberLatestActivities)
                .ToListAsync();
        }
    }
}

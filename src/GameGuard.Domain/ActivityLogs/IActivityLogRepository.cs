using System.Linq.Expressions;
using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs
{
    public interface IActivityLogRepository
    {
        Task<(IEnumerable<ActivityLog> Activities, int TotalCount)> GetAllAsync(
            ISpecification<ActivityLog> specification,
            int page,
            int pageSize
        );
        Task<ActivityLog?> GetByIdAsync(int activityId);
        Task AddAsync(ActivityLog activity);
        Task UpdateAsync(ActivityLog activity);
        Task<IList<ActivityLog>> GetRecentActivityLogsAsync(
            int playerId,
            int numberLatestActivities,
            ActivityActionType? activityActionType = null
        );
    }
}

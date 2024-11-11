using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Application.Common;

namespace GameGuard.Application.ActivityLogs
{
    public interface IActivityLogService
    {
        Task<PagedResult<ActivityLogDto>> GetActivityLogs(
            ActivityLogFilterDto filter,
            int page,
            int pageSize
        );
        Task AddActivityAsync(AddActivityLogDto addActivityLog);
        Task ReviewActivityAsync(int activityId, bool isSuspicious);
    }
}

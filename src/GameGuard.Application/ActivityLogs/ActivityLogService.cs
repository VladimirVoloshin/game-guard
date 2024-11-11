using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Application.ActivityLogs.Exceptions;
using GameGuard.Application.Common;
using GameGuard.Domain.ActivityLogs;

namespace GameGuard.Application.ActivityLogs
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityRepository;

        public ActivityLogService(IActivityLogRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<PagedResult<ActivityLogDto>> GetActivityLogs(
            ActivityLogFilterDto filter,
            int page,
            int pageSize
        )
        {
            var specification = new ActivityLogFilterSpecification(
                filter.PlayerIds,
                filter.IsSuspicious
            );

            var (activities, totalCount) = await _activityRepository.GetAllAsync(
                specification,
                page,
                pageSize
            );

            var activityDtos = activities
                .Select(a => new ActivityLogDto(
                    a.Id,
                    a.PlayerId,
                    a.Player?.Username ?? string.Empty,
                    a.Action.ToString(),
                    a.Timestamp,
                    a.IsSuspicious,
                    a.IsReviewed
                ))
                .ToList();

            return new PagedResult<ActivityLogDto>(activityDtos, totalCount, page, pageSize);
        }

        public async Task ReviewActivityAsync(int activityId, bool isSuspicious)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                throw new ActivityLogNotFoundException(activityId);
            }

            activity.MarkAsReviewed(isSuspicious);
            await _activityRepository.UpdateAsync(activity);
        }
    }
}

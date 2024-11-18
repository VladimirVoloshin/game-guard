using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Application.ActivityLogs.Exceptions;
using GameGuard.Application.Common;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Events;
using GameGuard.Domain.ActivityLogs.Specifications;
using MediatR;

namespace GameGuard.Application.ActivityLogs
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityRepository;
        private readonly IMediator _mediator;

        public ActivityLogService(IActivityLogRepository activityRepository, IMediator mediator)
        {
            _activityRepository = activityRepository;
            _mediator = mediator;
        }

        public async Task<PagedResult<ActivityLogDto>> GetActivityLogs(
            ActivityLogFilterDto filter,
            int page,
            int pageSize
        )
        {
            var specification = new ActivityLogFilterSpec(filter.PlayerIds, filter.IsSuspicious);

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

        public async Task AddActivityAsync(AddActivityLogDto addActivityLog)
        {
            var activity = new ActivityLog(addActivityLog.PlayerId, addActivityLog.Action);

            await _activityRepository.AddAsync(activity);

            await _mediator.Publish(new ActivityCreatedEvent(activity));
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

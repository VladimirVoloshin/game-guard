using GameGuard.Domain.ActivityLogs.Events;
using GameGuard.Domain.ActivityLogs.Specifications;
using GameGuard.Domain.Common.Specifications;
using MediatR;

namespace GameGuard.Domain.ActivityLogs
{
    public class ActivityMonitor : INotificationHandler<ActivityCreatedEvent>
    {
        private readonly Specification<ActivityLog> _suspiciousActivityCompositeSpecification;
        private readonly IActivityLogRepository _activityRepository;

        public ActivityMonitor(IActivityLogRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _suspiciousActivityCompositeSpecification =
                new RapidLevelCompletionSpecification(activityRepository)
                | new ConsecutiveLoginLogoutSpecification(activityRepository)
                | new RepeatedInvalidPasswordSpecification(activityRepository)
                | new MaxScoreSpecification();
        }

        public async Task Handle(
            ActivityCreatedEvent notification,
            CancellationToken cancellationToken
        )
        {
            bool isSuspicious = await DetectSuspiciousActivityAsync(notification.ActivityLog);
            if (isSuspicious)
            {
                await HandleSuspiciousActivityAsync(notification.ActivityLog);
            }
        }

        private async Task<bool> DetectSuspiciousActivityAsync(ActivityLog activityLog)
        {
            return await _suspiciousActivityCompositeSpecification.IsSatisfiedByAsync(activityLog);
        }

        private async Task HandleSuspiciousActivityAsync(ActivityLog activityLog)
        {
            activityLog.MarkAsSuspicious();
            await _activityRepository.UpdateAsync(activityLog);
        }
    }
}

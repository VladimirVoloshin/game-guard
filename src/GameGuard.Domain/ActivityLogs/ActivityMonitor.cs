using GameGuard.Domain.ActivityLogs.Events;
using GameGuard.Domain.ActivityLogs.Specifications;
using GameGuard.Domain.Common.Specifications;
using MediatR;

namespace GameGuard.Domain.ActivityLogs
{
    /// <summary>
    /// Monitors player activities for suspicious behavior using various detection strategies.
    /// This class combines multiple specifications to identify potential cheating, unauthorized access attempts,
    /// and other suspicious activities. It marks activities as suspicious when detected.
    /// </summary>
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

        private Task<bool> DetectSuspiciousActivityAsync(ActivityLog activityLog)
        {
            return _suspiciousActivityCompositeSpecification.IsSatisfiedByAsync(activityLog);
        }

        private Task HandleSuspiciousActivityAsync(ActivityLog activityLog)
        {
            activityLog.MarkAsSuspicious();
            return _activityRepository.UpdateAsync(activityLog);
        }
    }
}

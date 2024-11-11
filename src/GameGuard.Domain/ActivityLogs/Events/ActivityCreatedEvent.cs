using MediatR;

namespace GameGuard.Domain.ActivityLogs.Events
{
    public class ActivityCreatedEvent : INotification
    {
        public ActivityLog ActivityLog { get; }

        public ActivityCreatedEvent(ActivityLog activityLog)
        {
            ActivityLog = activityLog;
        }
    }
}

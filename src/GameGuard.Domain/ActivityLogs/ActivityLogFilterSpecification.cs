using System.Linq.Expressions;
using GameGuard.Domain.Common;

namespace GameGuard.Domain.ActivityLogs
{
    public class ActivityLogFilterSpecification : ISpecification<ActivityLog>
    {
        public IEnumerable<int> PlayerIds { get; }
        public bool? IsSuspicious { get; }

        public ActivityLogFilterSpecification(IEnumerable<int> playerIds, bool? isSuspicious)
        {
            PlayerIds = playerIds;
            IsSuspicious = isSuspicious;
        }

        public Expression<Func<ActivityLog, bool>> ToExpression()
        {
            return activityLog =>
                (PlayerIds == null || !PlayerIds.Any() || PlayerIds.Contains(activityLog.PlayerId))
                && (!IsSuspicious.HasValue || activityLog.IsSuspicious == IsSuspicious.Value);
        }
    }
}

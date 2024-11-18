using System.Linq.Expressions;
using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class ActivityLogFilterSpec : ISpecification<ActivityLog>
    {
        public IEnumerable<int>? PlayerIds { get; }
        public bool? IsSuspicious { get; }

        public ActivityLogFilterSpec(IEnumerable<int>? playerIds, bool? isSuspicious)
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

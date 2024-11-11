using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class MaxScoreSpecification : Specification<ActivityLog>
    {
        public override Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            return Task.FromResult(activityLog.Action == ActivityActionType.MaxScorePlayer);
        }
    }
}

using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    /// <summary>
    /// Identifies potential cheaters by flagging users who achieve the maximum score.
    /// This specification helps detect suspicious high-scoring activities.
    /// </summary>
    public class MaxScoreSpec : CompositeSpecification<ActivityLog>
    {
        public override Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            return Task.FromResult(activityLog.Action == ActivityActionType.MaxScorePlayer);
        }
    }
}

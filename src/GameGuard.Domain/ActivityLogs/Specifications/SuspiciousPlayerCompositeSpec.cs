using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class SuspiciousPlayerCompositeSpec : CompositeSpecification<ActivityLog>
    {
        private readonly CompositeSpecification<ActivityLog> _suspeciousSpecification;

        public SuspiciousPlayerCompositeSpec(IActivityLogRepository repository)
        {
            _suspeciousSpecification =
                new RapidLevelCompletionSpec(repository)
                | new ConsecutiveLoginLogoutSpec(repository)
                | new RepeatedInvalidPasswordSpec(repository)
                | new MaxScoreSpec();
        }

        public override Task<bool> IsSatisfiedByAsync(ActivityLog entity)
        {
            return _suspeciousSpecification.IsSatisfiedByAsync(entity);
        }
    }
}

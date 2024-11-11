using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class RepeatedInvalidPasswordSpecification : Specification<ActivityLog>
    {
        private const int RequiredInvalidAttempts = 3;
        private readonly IActivityLogRepository _repository;

        public RepeatedInvalidPasswordSpecification(IActivityLogRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            var recentLogs = await _repository.GetRecentActivityLogsAsync(
                activityLog.PlayerId,
                RequiredInvalidAttempts
            );
            return HasRequiredNumberOfLogs(recentLogs)
                && AllLogsAreInvalidPasswordAttempts(recentLogs);
        }

        private bool HasRequiredNumberOfLogs(IList<ActivityLog> logs)
        {
            return logs.Count == RequiredInvalidAttempts;
        }

        private bool AllLogsAreInvalidPasswordAttempts(IList<ActivityLog> logs)
        {
            return logs.All(x => x.Action == ActivityActionType.InvalidPassword);
        }
    }
}

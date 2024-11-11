using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    /// <summary>
    /// Detects potential brute force password attacks by identifying repeated invalid login attempts.
    /// Flags users who make multiple consecutive failed password entries, indicating possible unauthorized access attempts.
    /// </summary>
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

        private static bool HasRequiredNumberOfLogs(IList<ActivityLog> logs)
        {
            return logs.Count == RequiredInvalidAttempts;
        }

        private static bool AllLogsAreInvalidPasswordAttempts(IList<ActivityLog> logs)
        {
            return logs.All(x => x.Action == ActivityActionType.InvalidPassword);
        }
    }
}

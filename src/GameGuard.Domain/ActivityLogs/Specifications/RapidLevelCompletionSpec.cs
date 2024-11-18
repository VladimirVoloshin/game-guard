using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    /// <summary>
    /// Identifies potentially suspicious rapid level completions.
    /// Flags players who finish levels unusually quickly, possibly indicating cheating or exploitation.
    /// </summary>
    public class RapidLevelCompletionSpec : CompositeSpecification<ActivityLog>
    {
        private const int RequiredLogCount = 2;
        private const int RapidCompletionThresholdSeconds = 30;
        private readonly IActivityLogRepository _repository;

        public RapidLevelCompletionSpec(IActivityLogRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            if (NotLevelCompletionAction(activityLog))
                return false;

            var recentLogs = await GetRecentLevelCompletionLogsAsync(activityLog.PlayerId);

            if (NoSufficientRecentLogs(recentLogs))
                return false;

            return IsRapidCompletion(activityLog, recentLogs[1]);
        }

        private static bool NotLevelCompletionAction(ActivityLog log)
        {
            return log.Action != ActivityActionType.CompleteLevel;
        }

        private Task<IList<ActivityLog>> GetRecentLevelCompletionLogsAsync(int playerId)
        {
            return _repository.GetRecentActivityLogsAsync(
                playerId,
                RequiredLogCount,
                ActivityActionType.CompleteLevel
            );
        }

        private static bool NoSufficientRecentLogs(IList<ActivityLog> logs)
        {
            return logs.Count < RequiredLogCount;
        }

        private static bool IsRapidCompletion(ActivityLog currentLog, ActivityLog previousLog)
        {
            var timeDifference = currentLog.Timestamp - previousLog.Timestamp;
            return timeDifference.TotalSeconds < RapidCompletionThresholdSeconds;
        }
    }
}

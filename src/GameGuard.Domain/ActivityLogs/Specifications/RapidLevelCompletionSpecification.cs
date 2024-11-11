using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class RapidLevelCompletionSpecification : Specification<ActivityLog>
    {
        private const int RequiredLogCount = 2;
        private const int RapidCompletionThresholdSeconds = 30;
        private readonly IActivityLogRepository _repository;

        public RapidLevelCompletionSpecification(IActivityLogRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            if (NotLevelCompletionAction(activityLog))
                return false;

            var recentLogs = await GetRecentLevelCompletionLogs(activityLog.PlayerId);

            if (NoSufficientRecentLogs(recentLogs))
                return false;

            return IsRapidCompletion(activityLog, recentLogs[1]);
        }

        private bool NotLevelCompletionAction(ActivityLog log)
        {
            return log.Action != ActivityActionType.CompleteLevel;
        }

        private async Task<IList<ActivityLog>> GetRecentLevelCompletionLogs(int playerId)
        {
            return await _repository.GetRecentActivityLogsAsync(
                playerId,
                RequiredLogCount,
                ActivityActionType.CompleteLevel
            );
        }

        private bool NoSufficientRecentLogs(IList<ActivityLog> logs)
        {
            return logs.Count < RequiredLogCount;
        }

        private bool IsRapidCompletion(ActivityLog currentLog, ActivityLog previousLog)
        {
            var timeDifference = currentLog.Timestamp - previousLog.Timestamp;
            return timeDifference.TotalSeconds < RapidCompletionThresholdSeconds;
        }
    }
}

using GameGuard.Domain.Common.Specifications;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    public class ConsecutiveLoginLogoutSpecification : Specification<ActivityLog>
    {
        private const int RequiredLogCount = 4;
        private readonly IActivityLogRepository _repository;

        public ConsecutiveLoginLogoutSpecification(IActivityLogRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> IsSatisfiedByAsync(ActivityLog activityLog)
        {
            var recentLogs = await _repository.GetRecentActivityLogsAsync(
                activityLog.PlayerId,
                RequiredLogCount
            );

            if (recentLogs.Count < RequiredLogCount)
                return false;

            return IsConsecutiveLoginLogoutPattern(recentLogs);
        }

        private bool IsConsecutiveLoginLogoutPattern(IList<ActivityLog> logs)
        {
            for (int i = 0; i < RequiredLogCount - 1; i++)
            {
                if (NotLoginOrLogout(logs[i]))
                    return false;

                if (NotConsecutivePair(logs[i], logs[i + 1]))
                    return false;
            }

            return true;
        }

        private bool NotLoginOrLogout(ActivityLog log)
        {
            return log.Action != ActivityActionType.Login
                && log.Action != ActivityActionType.Logout;
        }

        private bool NotConsecutivePair(ActivityLog first, ActivityLog second)
        {
            return first.Action == second.Action;
        }
    }
}

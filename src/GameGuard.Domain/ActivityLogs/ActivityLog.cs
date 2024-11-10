using GameGuard.Domain.Players;

namespace GameGuard.Domain.ActivityLogs
{
    public class ActivityLog
    {
        public int Id { get; private set; }
        public int PlayerId { get; private set; }
        public ActivityActionType Action { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsSuspicious { get; private set; }
        public bool IsReviewed { get; private set; }
        public Player Player { get; private set; }
    }
}

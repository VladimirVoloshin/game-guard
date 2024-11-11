using GameGuard.Domain.Players;

namespace GameGuard.Domain.ActivityLogs
{
    public class ActivityLog
    {
        public ActivityLog(int playerId, ActivityActionType action, DateTime? timestamp = null)
        {
            PlayerId = playerId;
            Action = action;
            Timestamp = timestamp ?? DateTime.UtcNow;
            IsSuspicious = false;
            IsReviewed = false;
        }

        protected ActivityLog(int playerId, ActivityActionType action)
        {
            PlayerId = playerId;
            Action = action;
            Timestamp = DateTime.UtcNow;
            IsSuspicious = false;
            IsReviewed = false;
        }

        public int Id { get; private set; }
        public int PlayerId { get; private set; }
        public ActivityActionType Action { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsSuspicious { get; private set; }
        public bool IsReviewed { get; private set; }
        public Player Player { get; private set; }

        public void MarkAsReviewed(bool isSuspicious)
        {
            IsReviewed = true;
            IsSuspicious = isSuspicious;
        }

        public void MarkAsSuspicious()
        {
            IsSuspicious = true;
        }
    }
}

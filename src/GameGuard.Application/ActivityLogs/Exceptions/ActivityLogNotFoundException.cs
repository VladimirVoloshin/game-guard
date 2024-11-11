namespace GameGuard.Application.ActivityLogs.Exceptions
{
    public class ActivityLogNotFoundException : Exception
    {
        public ActivityLogNotFoundException(int activityId)
            : base($"Unable to find activityLog for id: {activityId}") { }
    }
}

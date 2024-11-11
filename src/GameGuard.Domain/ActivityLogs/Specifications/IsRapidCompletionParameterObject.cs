using System;

namespace GameGuard.Domain.ActivityLogs.Specifications
{
    internal class IsRapidCompletionParameterObject
    {
        public ActivityLog CurrentLog { get; set; }
        public ActivityLog PreviousLog { get; set; }

        public IsRapidCompletionParameterObject(ActivityLog currentLog, ActivityLog previousLog)
        {
            CurrentLog = currentLog;
            PreviousLog = previousLog;
        }
    }
}
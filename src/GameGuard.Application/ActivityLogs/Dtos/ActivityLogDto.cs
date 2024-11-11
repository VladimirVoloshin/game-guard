namespace GameGuard.Application.ActivityLogs.Dtos
{
    public record ActivityLogDto(
        int Id,
        int PlayerId,
        string PlayerUsername,
        string Action,
        DateTime Timestamp,
        bool IsSuspicious,
        bool IsReviewed
    );
}

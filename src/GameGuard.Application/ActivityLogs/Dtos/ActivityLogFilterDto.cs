namespace GameGuard.Application.ActivityLogs.Dtos
{
    public record ActivityLogFilterDto(IEnumerable<int>? PlayerIds, bool? IsSuspicious);
}

using GameGuard.Domain.ActivityLogs;

namespace GameGuard.Application.ActivityLogs.Dtos
{
    public record AddActivityLogDto(int PlayerId, ActivityActionType Action);
}

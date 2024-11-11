using GameGuard.Application.ActivityLogs.Dtos;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public interface IPlayerActivityGenerator
    {
        IEnumerable<AddActivityLogDto> CreateActivityLog(int playerId);
        PlayerGeneratorType PlayerBehaviorType { get; }
    }
}

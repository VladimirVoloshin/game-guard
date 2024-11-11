using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Domain.ActivityLogs;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public class CheaterPlayerActivityGenerator : IPlayerActivityGenerator
    {
        public PlayerGeneratorType PlayerBehaviorType => PlayerGeneratorType.Cheater;

        public IEnumerable<AddActivityLogDto> CreateActivityLog(int playerId)
        {
            return [new AddActivityLogDto(playerId, ActivityActionType.MaxScorePlayer)];
        }
    }
}

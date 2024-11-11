using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Domain.ActivityLogs;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public class BotPlayerActivityGenerator : IPlayerActivityGenerator
    {
        public PlayerGeneratorType PlayerBehaviorType => PlayerGeneratorType.Bot;

        public IEnumerable<AddActivityLogDto> CreateActivityLog(int playerId)
        {
            return
            [
                new AddActivityLogDto(playerId, ActivityActionType.CompleteLevel),
                new AddActivityLogDto(playerId, ActivityActionType.CompleteLevel),
            ];
        }
    }
}

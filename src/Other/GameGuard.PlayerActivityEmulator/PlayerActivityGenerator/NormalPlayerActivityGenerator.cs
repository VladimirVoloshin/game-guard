using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Domain.ActivityLogs;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public class NormalPlayerActivityGenerator : IPlayerActivityGenerator
    {
        private static readonly Random random = new Random();

        public PlayerGeneratorType PlayerBehaviorType => PlayerGeneratorType.Normal;

        public IEnumerable<AddActivityLogDto> CreateActivityLog(int playerId)
        {
            return [new AddActivityLogDto(playerId, (ActivityActionType)random.Next(1, 4))];
        }
    }
}

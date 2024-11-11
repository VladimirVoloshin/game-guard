using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Domain.ActivityLogs;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public class PasswordRememberingPlayerActivityGenerator : IPlayerActivityGenerator
    {
        public PlayerGeneratorType PlayerBehaviorType => PlayerGeneratorType.ForgotPassword;

        public IEnumerable<AddActivityLogDto> CreateActivityLog(int playerId)
        {
            return
            [
                new AddActivityLogDto(playerId, ActivityActionType.InvalidPassword),
                new AddActivityLogDto(playerId, ActivityActionType.InvalidPassword),
                new AddActivityLogDto(playerId, ActivityActionType.InvalidPassword)
            ];
        }
    }
}

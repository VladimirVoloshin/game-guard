using GameGuard.Application.Players.Dtos;
using GameGuard.Domain.Players;

namespace GameGuard.PlayerActivityEmulator.PlayerActivityGenerator
{
    public class PlayerActivityGeneratorFactory
    {
        private static readonly Random random = new Random();

        public PlayerActivityGeneratorFactory()
        {
            ActivityGenerators =
            [
                new NormalPlayerActivityGenerator(),
                new BotPlayerActivityGenerator(),
                new CheaterPlayerActivityGenerator()
            ];
        }

        public IPlayerActivityGenerator Create(PlayerDto player)
        {
            return (PlayerStatusType)player.StatusId switch
            {
                PlayerStatusType.Suspicious
                    => ActivityGenerators.First(x =>
                        x.PlayerBehaviorType == (PlayerGeneratorType)random.Next(2, 4)
                    ),
                _
                    => ActivityGenerators.First(x =>
                        x.PlayerBehaviorType == PlayerGeneratorType.Normal
                    )
            };
        }

        private IEnumerable<IPlayerActivityGenerator> ActivityGenerators { get; }
    }
}

using GameGuard.Application.Players.Dtos;
using GameGuard.Domain.Players;
using GameGuard.PlayerActivityEmulator.Clients;
using GameGuard.PlayerActivityEmulator.PlayerActivityGenerator;

namespace GameGuard.PlayerActivityEmulator
{
    public class LogGenerator
    {
        private readonly PlayerClient _playerClient;
        private readonly ActivityLogClient _activityLogClient;
        private readonly PlayerActivityGeneratorFactory _activityGeneratorFactory;

        public LogGenerator(
            PlayerClient playerClient,
            ActivityLogClient activityLogClient,
            PlayerActivityGeneratorFactory activityGeneratorFactory
        )
        {
            _playerClient = playerClient;
            _activityLogClient = activityLogClient;
            _activityGeneratorFactory = activityGeneratorFactory;
        }

        public async Task CreateLogs()
        {
            try
            {
                var players = await _playerClient.GetAllPlayers();

                foreach (var player in players)
                {
                    if (player.StatusId != (int)PlayerStatusType.Banned)
                    {
                        await AddActivityLog(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in iteration: {ex.Message}");
            }
        }

        private async Task AddActivityLog(PlayerDto player)
        {
            var activityGen = _activityGeneratorFactory.Create(player);
            var logs = activityGen.CreateActivityLog(player.Id);

            if (logs == null || !logs.Any())
            {
                Console.WriteLine("Unable to generate new activity logs");
                return;
            }

            foreach (var log in logs)
            {
                await _activityLogClient.AddActivityLog(log);
            }
        }
    }
}

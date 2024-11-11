using GameGuard.PlayerActivityEmulator;
using GameGuard.PlayerActivityEmulator.Clients;
using GameGuard.PlayerActivityEmulator.PlayerActivityGenerator;

class Program
{
    private const int ADD_LOGS_ITERATION_DELAY_SEC = 10;

    private static readonly string apiUrl =
        Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5063";
    private static readonly HttpClient httpClient = new HttpClient();

    private static readonly PlayerActivityGeneratorFactory _activityGeneratorFactory =
        new PlayerActivityGeneratorFactory();

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Using API URL: {apiUrl}");

        var playerService = new PlayerClient(apiUrl, httpClient);

        var activityLogService = new ActivityLogClient(apiUrl, httpClient);

        var logGenerator = new LogGenerator(
            playerService,
            activityLogService,
            _activityGeneratorFactory
        );

        while (true)
        {
            await logGenerator.CreateLogs();
            await Task.Delay(ADD_LOGS_ITERATION_DELAY_SEC * 1000);
        }
    }
}

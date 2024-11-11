using GameGuard.PlayerActivityEmulator;
using GameGuard.PlayerActivityEmulator.Clients;
using GameGuard.PlayerActivityEmulator.PlayerActivityGenerator;

class Program
{
    private const int ADD_LOGS_ITERATION_DELAY_SEC = 5;

    private static readonly string apiUrl =
        Environment.GetEnvironmentVariable("API_URL") ?? string.Empty;
    private static readonly HttpClient httpClient = new();

    private static readonly PlayerActivityGeneratorFactory _activityGeneratorFactory = new();

    static async Task Main(string[] args)
    {
        if (string.IsNullOrEmpty(apiUrl))
            Console.WriteLine("API_URL environment variable missing.");

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

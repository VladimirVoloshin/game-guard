using System.Net.Http.Json;
using GameGuard.Application.ActivityLogs.Dtos;

namespace GameGuard.PlayerActivityEmulator.Clients
{
    public class ActivityLogClient
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl;

        public ActivityLogClient(string apiUrl, HttpClient client)
        {
            _client = client;
            _apiUrl = apiUrl;
        }

        public async Task AddActivityLog(AddActivityLogDto log)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_apiUrl}/api/ActivityLogs", log);

                response.EnsureSuccessStatusCode();

                Console.WriteLine(
                    $"Added activity log for player {log.PlayerId}: Action {log.Action}"
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to add activity log due: {e.Message}");
            }
        }
    }
}

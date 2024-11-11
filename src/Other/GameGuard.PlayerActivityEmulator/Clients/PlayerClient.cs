using System.Text.Json;
using GameGuard.Application.Players.Dtos;

namespace GameGuard.PlayerActivityEmulator.Clients
{
    public class PlayerClient
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl;

        public PlayerClient(string apiUrl, HttpClient client)
        {
            _client = client;
            _apiUrl = apiUrl;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayers()
        {
            try
            {
                var response = await _client.GetAsync($"{_apiUrl}/api/Players");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<IEnumerable<PlayerDto>>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to fetch players due to: {e.Message}");
                return Enumerable.Empty<PlayerDto>();
            }
        }
    }
}

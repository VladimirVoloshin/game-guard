namespace GameGuard.Application.Players.Exceptions
{
    internal class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException(int playerId)
            : base($"Unable to find player for id: {playerId}") { }
    }
}

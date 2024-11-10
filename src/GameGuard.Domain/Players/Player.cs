namespace GameGuard.Domain.Players
{
    public class Player
    {
        public Player(string username)
        {
            Username = username;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public PlayerStatusType Status { get; private set; }
    }
}

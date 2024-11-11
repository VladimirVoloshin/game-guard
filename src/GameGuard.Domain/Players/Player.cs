namespace GameGuard.Domain.Players
{
    public class Player
    {
        public Player(int id, string username, PlayerStatusType status)
        {
            Id = id;
            Username = username;
            Status = status;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public PlayerStatusType Status { get; private set; }

        public void UpdateStatus(PlayerStatusType newStatus)
        {
            Status = newStatus;
        }
    }
}

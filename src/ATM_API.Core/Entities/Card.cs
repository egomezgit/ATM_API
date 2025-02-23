namespace ATM_API.Core.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string PINHash { get; set; }
        public bool IsBlocked { get; set; }
        public int FailedAttempts { get; set; }
    }
}


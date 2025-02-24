namespace ATM_API.Application.DTOs
{
    public class BalanceResponseDto
    {
        public string UserName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastTransactionDate { get; set; }
    }
}


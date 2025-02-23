namespace ATM_API.Application.DTOs.Transaction
{
    public class WithdrawRequestDto
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}

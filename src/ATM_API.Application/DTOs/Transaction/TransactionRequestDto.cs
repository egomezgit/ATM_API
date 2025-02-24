namespace ATM_API.Application.DTOs.Transaction
{
    public class TransactionRequestDto
    {
        /// <summary>
        /// Card number
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// Amount to be withdrawn
        /// </summary>
        public decimal Amount { get; set; }


    }
}

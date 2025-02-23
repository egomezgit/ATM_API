namespace ATM_API.Application.DTOs.Transaction
{
    public class WithdrawResponsetDto
    {
        /// <summary>
        /// Balance after withdrawal
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// Amount withdrawn
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the last withdrawal
        /// </summary>
        public DateTime LastWithdrawalDate { get; set; }



    }
}

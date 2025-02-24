namespace ATM_API.Application.DTOs.Transaction
{
    public class TransactionResponseDto
    {   
        /// <summary>
        /// Indicates if the transaction was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// New balance after transaction
        /// </summary>
        public decimal NewBalance { get; set; }

        /// <summary>
        /// Message to be displayed to the user
        /// </summary>
        public string Message { get; set; }



    }
}

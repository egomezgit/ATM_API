namespace ATM_API.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        /// <summary>
        /// Card number
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// PIN
        /// </summary>
        public string PIN { get; set; }
    }
}


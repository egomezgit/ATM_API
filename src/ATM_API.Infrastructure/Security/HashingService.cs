using System.Security.Cryptography;
using ATM_API.Application.Interfaces.Security;

namespace ATM_API.Infrastructure.Security
{
    public class HashingService : IHashingService
    {
        public string Hash(string input)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(input, 16, 10000, HashAlgorithmName.SHA256))
            {
                byte[] salt = pbkdf2.Salt;
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] result = new byte[48];

                Buffer.BlockCopy(salt, 0, result, 0, 16);
                Buffer.BlockCopy(hash, 0, result, 16, 32);

                return Convert.ToBase64String(result);
            }
        }

        public bool VerifyHash(string input, string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] newHash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (newHash[i] != hashBytes[i + 16])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}



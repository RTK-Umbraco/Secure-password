using Secure_password.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace Secure_password.Managements
{
    public class HashService
    {
        /// <summary>
        /// Create SHA256 with a random salt
        /// </summary>
        /// <param name="password">Users plain text password</param>
        /// <returns>Returns a password DTO that include the hashedpassword and the generated salt</returns>
        public PasswordDto ComputeHashSha256WithSalt(string password)
        {
            var toBeHashed = Encoding.UTF8.GetBytes(password);
            var salt = GenerateSalt();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassowrd = sha256.ComputeHash(Combine(toBeHashed, salt));
                PasswordDto passwordDto = new PasswordDto(hashedPassowrd, salt);
                
                return passwordDto;
            }
        }

        /// <summary>
        /// Create SHA256 with a specific salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedSalt"></param>
        /// <returns>Returns a passwordDto that include the hashedpassword and the specific salt</returns>
        public PasswordDto ComputeHashSha256WithStoredSalt(string password, string storedSalt)
        {
            var toBeHashed = Encoding.UTF8.GetBytes(password);
            var salt = Convert.FromBase64String(storedSalt);

            using (var sha256 = SHA256.Create())
            {
                var hashedPassowrd = sha256.ComputeHash(Combine(toBeHashed, salt));
                PasswordDto passwordDto = new PasswordDto(hashedPassowrd);

                return passwordDto;
            }
        }

        private byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        private byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }
}

using SimpleCrypto;

namespace FarmPhoto.Common.Cryptography
{
    public class Cryptography : ICryptography
    {
        /// <summary>
        /// The _crypto service service
        /// </summary>
        private readonly ICryptoService _cryptoServiceService = new PBKDF2();

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public string HashPassword(string password, string salt)
        {

            return _cryptoServiceService.Compute(password, salt);
        }

        /// <summary>
        /// Generates the password salt.
        /// </summary>
        /// <returns></returns>
        public string GeneratePasswordSalt()
        {
            return _cryptoServiceService.GenerateSalt();
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="passwordAttempt">The password attempt.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="passwordFromDatabase">The password from database.</param>
        /// <returns></returns>
        public bool ValidatePassword(string passwordAttempt, string salt, string passwordFromDatabase)
        {
            return passwordFromDatabase.Equals(_cryptoServiceService.Compute(passwordAttempt, salt));
        }
    }
}

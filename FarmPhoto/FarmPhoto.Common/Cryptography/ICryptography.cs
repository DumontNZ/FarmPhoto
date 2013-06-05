namespace FarmPhoto.Common.Cryptography
{
    public interface ICryptography
    {
        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        string HashPassword(string password, string salt);

        /// <summary>
        /// Generates the password salt.
        /// </summary>
        /// <returns></returns>
        string GeneratePasswordSalt();

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="passwordAttempt">The password attempt.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="passwordFromDatabase">The password from database.</param>
        /// <returns></returns>
        bool ValidatePassword(string passwordAttempt, string salt, string passwordFromDatabase);
    }
}
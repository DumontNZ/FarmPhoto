using FarmPhoto.Domain;

namespace FarmPhoto.Core.Authentication
{
    public interface IFormsAuthenticationManager
    {
        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userToValidate">The user to validate.</param>
        /// <returns></returns>
        bool ValidateUser(User userToValidate);

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="remember">if set to <c>true</c> remeber the user the next time they come back.</param>
        void Login(User user, bool remember);

        void Logout();
    }
}
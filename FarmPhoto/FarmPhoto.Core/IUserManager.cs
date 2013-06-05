using System.Collections.Generic;
using FarmPhoto.Domain;

namespace FarmPhoto.Core
{
    public interface IUserManager
    {
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns></returns>
        int CreateUser(User user);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        User GetUser(User user);
    }
}
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
        int Create(User user);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> Get();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        User Get(User user);

        /// <summary>
        /// Gets the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        User Get(int userId);
    }
}
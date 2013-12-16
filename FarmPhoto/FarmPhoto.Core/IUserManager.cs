using System;
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

        /// <summary>
        /// Gets the specified user by emailAddress.
        /// </summary>
        /// <param name="emailAddress">The user emailAddress</param>
        /// <returns></returns>
        User Get(string emailAddress);

        void CreateToken(User user, TimeSpan timeSpan);

        bool CheckToken(string username, string token);

        bool CheckToken(User user, string token);

        void UpdatePasswordWithToken(User user, string password, string token);

        ProfileInformation GetProfileInformation(string username);
    }
}
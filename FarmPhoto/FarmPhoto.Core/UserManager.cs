using FarmPhoto.Domain;
using FarmPhoto.Repository;
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
          
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns></returns>
        public int Create(User user)
        {
            return _userRepository.Create(user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return _userRepository.Get();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Get(User user)
        {
            return _userRepository.Get(user);
        }

        /// <summary>
        /// Gets the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public User Get(int userId)
        {
            return _userRepository.Get(userId);
        }
    }
}

using System;
using System.Security;
using FarmPhoto.Common.Cryptography;
using FarmPhoto.Common.Extensions;
using FarmPhoto.Core.Extensions;
using FarmPhoto.Domain;
using FarmPhoto.Repository;
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public class UserManager : IUserManager
    {
        private readonly ICryptography _cryptography;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="cryptography"></param>
        public UserManager(IUserRepository userRepository, ICryptography cryptography)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns></returns>
        public int Create(User user)
        {
            string passwordSalt = _cryptography.GeneratePasswordSalt();

            user.PasswordSalt = passwordSalt;
            user.Password = _cryptography.HashPassword(user.Password, passwordSalt);

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

        public User Get(string emailAddress)
        {
            return _userRepository.Get(emailAddress);
        }

        public void CreateToken(User user, TimeSpan timeSpan)
        {
            var salt = _cryptography.GeneratePasswordSalt();
            user.Token = _cryptography.HashPassword(Guid.NewGuid().ToString(), salt).RemoveSpecialCharacters();
            user.TokenExpiry = DateTime.UtcNow.AddHours(timeSpan.TotalHours);

            _userRepository.CreateToken(user);
        }

        public bool CheckToken(string username, string token)
        {
            var user = Get(new User {UserName =  username});
            return CheckToken(user, token); 
        }

        public bool CheckToken(User user, string token)
        {
            return user.HasValidToken(token); 
        }

        public void UpdatePasswordWithToken(User user, string password, string token)
        {
            if (!user.HasValidToken(token))
            {
                throw new SecurityException("The token is invalid; or the token has expired.");
            }

            string passwordSalt = _cryptography.GeneratePasswordSalt();

            user.PasswordSalt = passwordSalt;
            user.Password = _cryptography.HashPassword(password, passwordSalt);

            _userRepository.UpdatePassword(user);
        }
    }
}

using System;
using FarmPhoto.Domain;
using NUnit.Framework;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class UserTests
    {
        private readonly UserRepository _userRepository;

        public UserTests()
        {
            _userRepository = new UserRepository();
        }

        [Test]
        public void CreateUserSuccessfully()
        {
            var user = new User
                {
                    UserName = "FrankTheTank",
                    Country = "New Zealand",
                    Email = "joel_tennant@hotmail.com",
                    Password = new Guid().ToString(),
                    FirstName = "Frank",
                    Surname = "Jimmy",
                    PasswordSalt = new Guid().ToString()
                };

            Assert.IsTrue(_userRepository.Create(user) > 1);
        }

        [Test]
        public void GetUserSuccessfully()
        {
            var user = new User { UserName = "DumontNZ" };

            var username = _userRepository.GetUser(user).UserName;

            Assert.IsNotNull(username);
        }
    }
}

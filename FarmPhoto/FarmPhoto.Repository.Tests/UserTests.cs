using System;
using FarmPhoto.Domain;
using NUnit.Framework;
using FarmPhoto.Common.Configuration;
using Ninject.Extensions.Logging;
using log4net.Repository.Hierarchy;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class UserTests : TestBase
    {
        //private readonly UserRepository _userRepository;

        //public UserTests()
        //{
        //    _userRepository = new UserRepository(new Config(), new Logger() );
        //}

        //[Test]
        //public void CreateUserSuccessfully()
        //{
        //    var user = new User
        //        {
        //            UserName = "FrankTheTank",
        //            Country = "New Zealand",
        //            Email = "joel_tennant@hotmail.com",
        //            Password = new Guid().ToString(),
        //            FirstName = "Frank",
        //            Surname = "Jimmy",
        //            PasswordSalt = new Guid().ToString()
        //        };

        //    Assert.IsTrue(_userRepository.Create(user) > 1);
        //}

        //[Test]
        //public void GetUserSuccessfully()
        //{
        //    var user = new User { UserName = "DumontNZ" };

        //    var username = _userRepository.Get(user).UserName;

        //    Assert.IsNotNull(username);
        //}
    }
}

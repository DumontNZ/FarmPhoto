﻿using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using FarmPhoto.Common.Resources;
using Ninject.Extensions.Logging;
using FarmPhoto.Common.Cryptography;
using FarmPhoto.Core.Authentication;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IConfig _config;
        private readonly ILogger _logger;
        private readonly ICryptography _cryptography;
        private readonly IFormsAuthenticationManager _formsAuthenticationManager;

        public AccountController(IUserManager userManager, IConfig config, ILogger logger, ICryptography cryptography, IFormsAuthenticationManager formsAuthenticationManager)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _cryptography = cryptography;
            _formsAuthenticationManager = formsAuthenticationManager;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            _logger.Debug("SurveyController Index post");

            if (ModelState.IsValid)
            {
                bool successfullyValidated = _formsAuthenticationManager.ValidateUser(new User { UserName = loginModel.Username, Password = loginModel.Password });
                if (successfullyValidated)
                {
                    _formsAuthenticationManager.Login(_userManager.GetUser(new User{UserName = loginModel.Username}), loginModel.RememberMe);
                    return RedirectToAction("Index", "Submission");
                }
                ModelState.AddModelError("UserName", ErrorMessages.InvalidUserNameOrPassword);
            }

            return View(new LoginModel());
        }

        [AllowAnonymous]
        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.GetUser(new User { UserName = userModel.Username }) != null)
                {
                    ModelState.AddModelError("Username", ErrorMessages.UsernameInUse);
                }

                if (ModelState.IsValid)
                {
                    string passwordSalt = _cryptography.GeneratePasswordSalt();

                    var user = new User
                    {
                        UserName = userModel.Username,
                        FirstName = userModel.FirstName,
                        Surname = userModel.Surname,
                        Country = userModel.Country,
                        Email = userModel.EmailAddress,
                        PasswordSalt = passwordSalt,
                        Password = _cryptography.HashPassword(userModel.Password, passwordSalt)
                    };

                    _userManager.CreateUser(user);

                   // UserMailer.Welcome(user).Send();

                    return View("Login");
                }
            }

            return View(userModel);
        }
    }
}

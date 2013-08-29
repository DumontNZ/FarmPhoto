using System;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Core;
using FarmPhoto.Website.Models;
using FarmPhoto.Website.Mailers;
using FarmPhoto.Common.Resources;
using Ninject.Extensions.Logging;
using FarmPhoto.Core.Authentication;
using FarmPhoto.Common.Configuration;
using FarmPhoto.Website.Core.Resources;

namespace FarmPhoto.Website.Controllers
{
    public class AccountController : FarmPhotoControllerBase
    {
        private readonly IConfig _config;
        private readonly ILogger _logger;
        private readonly IUserManager _userManager;
        private readonly IFormsAuthenticationManager _formsAuthenticationManager;
        private IUserMailer _userMailer = new UserMailer();

        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

        public AccountController(IUserManager userManager, IConfig config, IFormsAuthenticationManager formsAuthenticationManager, ILogger logger)
        {
            _config = config;
            _userManager = userManager;
            _formsAuthenticationManager = formsAuthenticationManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnURL"];

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool successfullyValidated = _formsAuthenticationManager.ValidateUser(new User { UserName = loginModel.Username.ToLower(), Password = loginModel.Password });
                if (successfullyValidated)
                {
                    _formsAuthenticationManager.Login(_userManager.Get(new User { UserName = loginModel.Username.ToLower() }), loginModel.RememberMe);

                    if (Request.QueryString["ReturnURL"] != null)
                    {
                        return Redirect(Request.QueryString["ReturnURL"]);
                    }

                    return RedirectToAction("MyPhotos", "Gallery");
                }
            }

            SetMessage(MessageKey.InvalidUsernameOrPassword);

            return View(new LoginModel());
        }

        public ActionResult Logout()
        {
            _formsAuthenticationManager.Logout();

            SetRedirectMessage(MessageKey.LoggedOut);

            return RedirectToAction("Index", "Home");
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
                if (!string.IsNullOrWhiteSpace(_userManager.Get(new User { UserName = userModel.Username.ToLower() }).UserName))
                {
                    ModelState.AddModelError("Username", ErrorMessages.UsernameInUse);
                }

                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        UserName = userModel.Username.ToLower(),
                        DisplayName = userModel.Username,
                        FirstName = userModel.FirstName,
                        Surname = userModel.Surname,
                        Country = userModel.Country,
                        Email = userModel.EmailAddress,
                        Password = userModel.Password
                    };

                    _userManager.Create(user);

                    // UserMailer.Welcome(user).Send();

                    SetRedirectMessage(MessageKey.AccountCreated);

                    return RedirectToAction("Login");
                }
            }

            return View(userModel);
        }

        [AllowAnonymous]
        public ActionResult ForgottenPassword()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgottenPassword(ForgottenPasswordModel forgottenPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgottenPasswordModel);
            }

            var user = _userManager.Get(new User { UserName = forgottenPasswordModel.Username.ToLower() });
            _logger.Info("Model.usernname " + forgottenPasswordModel.Username + "User email address " + user.Email);
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                _userManager.CreateToken(user, new TimeSpan(0, 3, 0, 0));
                _logger.Info("After Creating Token: Model.usernname " + forgottenPasswordModel.Username + "User email address " + user.Email);
                try
                {
                    UserMailer.PasswordReset(user, new Uri(Url.Action("ResetPassword", null, null, Request.Url.Scheme))).Send();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);

                }
            }

            SetRedirectMessage(MessageKey.PasswordResetEmailSent);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string u, string t)
        {
            var username = u.ToLower();
            if (_userManager.CheckToken(username, t))
            {
                return View(new PasswordResetModel { Username = username, HasValidToken = true, Token = t }); 
            }

            SetMessage(MessageKey.InvalidToken);

            return View(new PasswordResetModel { HasValidToken = false });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(PasswordResetModel model)
        {
            model.HasValidToken = true;

            if (ModelState.IsValid)
            {
                var user = _userManager.Get(new User {UserName = model.Username.ToLower()});

                if (_userManager.CheckToken(user, model.Token))
                {
                    _userManager.UpdatePasswordWithToken(user, model.Password, model.Token); 

                    SetRedirectMessage(MessageKey.PasswordHasBeenReset);

                    return RedirectToAction("Login"); 
                }

                model.HasValidToken = false;

                SetMessage(MessageKey.InvalidToken);
            }

            return View(model);
        }
    }
}

using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using FarmPhoto.Common.Resources;
using FarmPhoto.Common.Cryptography;
using FarmPhoto.Core.Authentication;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfig _config;
        private readonly IUserManager _userManager;
        private readonly ICryptography _cryptography;
        private readonly IFormsAuthenticationManager _formsAuthenticationManager;

        public AccountController(IUserManager userManager, IConfig config, ICryptography cryptography, IFormsAuthenticationManager formsAuthenticationManager)
        {
            _config = config;
            _userManager = userManager;
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
            if (ModelState.IsValid)
            {
                bool successfullyValidated = _formsAuthenticationManager.ValidateUser(new User { UserName = loginModel.Username.ToLower(), Password = loginModel.Password });
                if (successfullyValidated)
                {
                    _formsAuthenticationManager.Login(_userManager.Get(new User{UserName = loginModel.Username.ToLower()}), loginModel.RememberMe);
                    return RedirectToAction("MyPhotos", "Gallery");
                }
                ModelState.AddModelError("UserName", ErrorMessages.InvalidUserNameOrPassword);
            }

            return View(new LoginModel());
        }

        public ActionResult Logout()
        {
            _formsAuthenticationManager.Logout();

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
                if (_userManager.Get(new User { UserName = userModel.Username.ToLower() }).UserName != null)
                {
                    ModelState.AddModelError("Username", ErrorMessages.UsernameInUse);
                }

                if (ModelState.IsValid)
                {
                    string passwordSalt = _cryptography.GeneratePasswordSalt();

                    var user = new User
                    {
                        UserName = userModel.Username.ToLower(),
                        DisplayName = userModel.Username, 
                        FirstName = userModel.FirstName,
                        Surname = userModel.Surname,
                        Country = userModel.Country,
                        Email = userModel.EmailAddress,
                        PasswordSalt = passwordSalt,
                        Password = _cryptography.HashPassword(userModel.Password, passwordSalt)
                    };

                    _userManager.Create(user);

                   // UserMailer.Welcome(user).Send();

                    return RedirectToAction("Login");
                }
            }

            return View(userModel);
        }
    }
}

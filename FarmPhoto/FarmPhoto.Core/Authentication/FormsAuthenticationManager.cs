using System;
using System.Web;
using FarmPhoto.Domain;
using System.Web.Security;
using System.Globalization;
using System.Collections.Generic;
using FarmPhoto.Common.Cryptography;
using FarmPhoto.Common.Configuration;


namespace FarmPhoto.Core.Authentication
{
    public class FormsAuthenticationManager : IFormsAuthenticationManager
    {
        private readonly ICryptography _cryptography;
        private readonly IUserManager _userManager;
        private readonly IConfig _config;

        public FormsAuthenticationManager(ICryptography cryptography, IUserManager userManager, IConfig config)
        {
            _cryptography = cryptography;
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userToValidate">The user to validate.</param>
        /// <returns></returns>
        public bool ValidateUser(User userToValidate)
        {
            User user = _userManager.GetUser(userToValidate);

            return _cryptography.ValidatePassword(userToValidate.Password, user.PasswordSalt, user.Password);
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="remember">if set to <c>true</c> remeber the user the next time they come back.</param>
        public void Login(User user, bool remember)
        {
            var data = new List<string>();

            DateTime issued = DateTime.UtcNow;

            DateTime expiration = remember
                                      ? issued.AddMinutes(_config.YearInMinutes)
                                      : issued.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);

            data.InsertRange(0, new List<string>{user.FirstName, user.Surname, user.UserId.ToString(CultureInfo.InvariantCulture)});                
            
            var ticket = new FormsAuthenticationTicket(1, user.UserName, issued, expiration,
                                                       true, string.Join(",", data));
           
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)){Expires = expiration});

        }
    }
}

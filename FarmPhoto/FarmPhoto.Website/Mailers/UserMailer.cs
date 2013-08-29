using System;
using FarmPhoto.Core.Extensions;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using Mvc.Mailer;

namespace FarmPhoto.Website.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
                x.Subject = "Welcome " + DateTime.UtcNow.ToShortTimeString() + " Port Number = 587";
				x.ViewName = "Welcome";
				x.To.Add("joel.tennant@intergen.co.nz");
			});
		}

        public virtual MvcMailMessage PasswordReset(User user, Uri uri)
        {
            var passwordResetUrl = new Uri(uri, user.GetEmailQueryStringValue()).ToString();

            var passwordResetModel = new EmailPasswordResetModel
                {
                    FirstName = user.FirstName,
                    PasswordResetLink = passwordResetUrl
                };

            ViewBag.Data = passwordResetModel;

			return Populate(x =>
			{
				x.Subject = "Password Reset";
				x.ViewName = "PasswordReset";
                x.To.Add(user.Email);
			});
		}
 	}
}
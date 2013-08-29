using System;
using FarmPhoto.Domain;
using Mvc.Mailer;

namespace FarmPhoto.Website.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome();
			MvcMailMessage PasswordReset(User user, Uri url);
	}
}
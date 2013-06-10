using System.Web;
using System.Web.Security;
using System.Security.Authentication;

namespace FarmPhoto.Domain
{
    public static class CurrentUser
    {
        public static int Id {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie != null)
                {
                    var authTicket = FormsAuthentication.Decrypt(cookie.Value);

                    if (authTicket != null)
                    {
                        string[] data = authTicket.UserData.Split(new[] {','});

                        return int.Parse(data[2]);
                    }
                }

                throw new AuthenticationException("User must be logged in to access this page");
            } 
        }
    }
}

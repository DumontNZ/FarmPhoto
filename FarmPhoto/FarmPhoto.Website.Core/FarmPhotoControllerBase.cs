using System;
using System.Web;
using System.Web.Mvc;
using FarmPhoto.Website.Core.Resources;

namespace FarmPhoto.Website.Core
{
    public abstract class FarmPhotoControllerBase : Controller 
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = Request.Cookies.Get(MessageResolver.MessageCookieName);

            if (cookie != null)
            {
                ViewBag.Message = MessageResolver.Resolve(cookie.Value);

                cookie.Expires = DateTime.UtcNow.AddYears(-1);

                Response.Cookies.Set(cookie);
            }

            base.OnActionExecuting(filterContext);
        }

        protected void SetMessage(MessageKey key)
        {
            ViewBag.Message = MessageResolver.Resolve(key);
        }

        protected void SetRedirectMessage(MessageKey key)
        {
            Response.Cookies.Add(new HttpCookie(MessageResolver.MessageCookieName, key.ToString())
            {
                Expires = DateTime.UtcNow.AddMinutes(1)
            });
        }
    }
}

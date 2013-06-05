using System;
using System.Web.Mvc;
using FarmPhoto.Website.Content;

namespace FarmPhoto.Website.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FarmPhotoAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var actionDefined = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            var controllerDefined = filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (actionDefined || controllerDefined)
            {
                return;
            }

            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(Urls.ErrorPage);
            }
            base.OnAuthorization(filterContext);
        }
    }
}
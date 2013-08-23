using System;
using System.Web.Mvc;
using System.Web.Mvc.Html; 

namespace FarmPhoto.Website.Extensions
{
    public static class MenuExtensions
    {
        public static MvcHtmlString MenuItemAction(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues = null, object htmlAttributes = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active"); 
            }

            if (routeValues != null)
            {
                li.InnerHtml = (htmlAttributes != null) ? htmlHelper.ActionLink(linkText,
                                            action,
                                            controller,
                                            routeValues,
                                            htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(linkText,
                                            action,
                                            controller,
                                            routeValues).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.ActionLink(linkText,
                                                     action,
                                                     controller).ToHtmlString();
            }
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItemRoute(this HtmlHelper htmlHelper, string linkText, string routeName, string action, string controller, object routeValues = null, object htmlAttributes = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }

            if (routeValues != null)
            {
                li.InnerHtml = (htmlAttributes != null) ? htmlHelper.RouteLink(linkText,
                                            routeName,
                                            routeValues,
                                            htmlAttributes).ToHtmlString()
                    : htmlHelper.RouteLink(linkText,
                                            routeName,
                                            routeValues).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.RouteLink(linkText,
                                                     routeName).ToHtmlString();
            }
            return MvcHtmlString.Create(li.ToString());
        }

    }
}
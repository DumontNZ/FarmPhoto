using System.Web.Mvc;
using System.Web.Routing;

namespace FarmPhoto.Website.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Username",
                url: "{controller}/{action}/{username}",
                defaults: new { controller = "Gallery", action = "UsersPhotos", username = UrlParameter.Optional }
            );
        }
    }
}
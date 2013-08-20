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
                name: "GalleryPage",
                url: "Gallery/Index/{page}",
                defaults: new { controller = "Gallery", action = "Index" }
                );

            routes.MapRoute(
                name: "Username",
                url: "Gallery/UsersPhotos/{username}",
                defaults: new { controller = "Gallery", action = "UsersPhotos", username = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tags",
                url: "Gallery/Tag/{tag}",
                defaults: new { controller = "Gallery", action = "Tag", tag = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
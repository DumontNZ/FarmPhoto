using System.Web.Mvc;
using FarmPhoto.Domain;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ViewBag.User = CurrentUser.Id; 
            }

            return View();
        }
    }
}

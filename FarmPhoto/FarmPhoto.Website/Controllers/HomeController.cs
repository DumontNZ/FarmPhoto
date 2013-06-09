using System.Web.Mvc;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }
    }
}

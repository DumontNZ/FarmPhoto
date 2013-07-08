using System.Web.Mvc;
using FarmPhoto.Domain;

namespace FarmPhoto.Website.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (CurrentUser.IsAdministrator)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

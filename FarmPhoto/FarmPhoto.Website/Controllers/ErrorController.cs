using System.Web.Mvc;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(ErrorModel errorModel)
        {

            return View(errorModel);
        }
    }
}

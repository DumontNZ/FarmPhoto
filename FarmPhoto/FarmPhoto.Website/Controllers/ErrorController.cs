using System.Web.Mvc;
using FarmPhoto.Website.Core;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class ErrorController : FarmPhotoControllerBase
    {
        //
        // GET: /Error/

        public ActionResult Index(ErrorModel errorModel)
        {

            return View(errorModel);
        }
    }
}

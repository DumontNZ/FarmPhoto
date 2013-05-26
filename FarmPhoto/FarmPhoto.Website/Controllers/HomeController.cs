using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoManager _photoManager;

        public HomeController()
        {
        }

        public HomeController(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            _photoManager.CreatePhoto(new Photo {Title = "Title"});

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

using System.IO;
using System.Web;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly PhotoManager _photoManager;

        public SubmissionController(PhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var memoryStream = new MemoryStream();

                file.InputStream.CopyTo(memoryStream);

                byte[] imageAsByte = memoryStream.ToArray();

                _photoManager.CreatePhoto(new Photo {PhotoData = imageAsByte});
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        public ActionResult TheImage(SubmissionModel model)
        {

            return View(model); 
        }
    }
}

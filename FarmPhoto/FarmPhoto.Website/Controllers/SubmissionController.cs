using System;
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SubmissionModel model)
        {
            if (ModelState.IsValid)
            {
                var memoryStream = new MemoryStream();

                model.File.InputStream.CopyTo(memoryStream);

                byte[] imageAsByte = memoryStream.ToArray();

                var photo = new Photo
                    {
                        Title = model.Title,
                        Description = model.Description,
                        PhotoData = imageAsByte,
                        ImageType = model.File.ContentType,
                        FileSize = model.File.ContentLength,
                        UserId = CurrentUser.Id
                    };

                var photoId = _photoManager.CreatePhoto(photo);
            }

            return RedirectToAction("MyImages", "Gallery");
        }

        public ActionResult TheImage(SubmissionModel model)
        {

            return View(model);
        }
    }
}

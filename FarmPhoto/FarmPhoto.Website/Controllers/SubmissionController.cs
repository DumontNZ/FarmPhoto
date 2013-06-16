using System.IO;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ITagManager _tagManager;
        private readonly IPhotoManager _photoManager;

        public SubmissionController(IPhotoManager photoManager, ITagManager tagManager)
        {
            _photoManager = photoManager;
            _tagManager = tagManager;
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
                _tagManager.CreateTag(model.Tags, photoId);
            }

            return RedirectToAction("MyImages", "Gallery");
        }

        public ActionResult TheImage(SubmissionModel model)
        {

            return View(model);
        }
    }
}

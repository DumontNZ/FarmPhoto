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
                var photo = new Photo
                    {
                        Title = model.Title,
                        Description = model.Description,
                        ImageType = model.File.ContentType,
                        UserId = CurrentUser.Id
                    };
                var photoId = _photoManager.CreatePhoto(photo, model.File);

                _tagManager.CreateTag(model.Tags, photoId);
            }

            return RedirectToAction("MyPhotos", "Gallery");
        }
    }
}

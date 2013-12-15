using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Website.Core;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : FarmPhotoControllerBase
    {
        private readonly PhotoManager _photoManager;
        private readonly UserManager _userManager;

        public HomeController(PhotoManager photoManager, UserManager userManager)
        {
            _photoManager = photoManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {

            var photos = _photoManager.Get(1, 5);
            
            var galleryModel = new GalleryModel();

            foreach (var photo in photos)
            {
                var photoModel = new PhotoModel
                {
                    PhotoId = photo.PhotoId,
                    UserId = photo.UserId,
                    Description = photo.Description,
                    Title = photo.Title,
                    FileName = photo.FileName,
                    SubmittedOn = photo.CreatedOnDateUtc
                };

                galleryModel.PhotoModels.Add(photoModel);
            }
            
            return View(galleryModel);
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {

            return View();
        }
    }
}

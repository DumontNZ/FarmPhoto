using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
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
            
            var mainPhoto = _photoManager.Get(0, false);

            var user = _userManager.Get(mainPhoto.UserId);

            var galleryModel = new GalleryModel();

            var mainPhotoModel = new PhotoModel
            {
                PhotoId = mainPhoto.PhotoId,
                UserId = mainPhoto.UserId,
                Description = mainPhoto.Description,
                Title = mainPhoto.Title,
                SubmittedBy = user.UserName,
                SubmittedOn = mainPhoto.CreatedOnDateUtc
            };

            galleryModel.PhotoModels.Add(mainPhotoModel);

            foreach (var photo in photos)
            {
                var photoModel = new PhotoModel
                {
                    PhotoId = photo.PhotoId,
                    UserId = photo.UserId,
                    Description = photo.Description,
                    Title = photo.Title,
                    SubmittedOn = photo.CreatedOnDateUtc
                };

                galleryModel.PhotoModels.Add(photoModel);
            }
            
            return View(galleryModel);
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {

            return View();
        }
    }
}

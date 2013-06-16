using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using System.Collections.Generic;

namespace FarmPhoto.Website.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ITagManager _tagManager;
        private readonly IPhotoManager _photoManager;

        public GalleryController(IPhotoManager photoManager, ITagManager tagManager)
        {
            _photoManager = photoManager;
            _tagManager = tagManager;
        }

        /// <summary>
        /// Returns all approved images as a gallery newest to oldest
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IList<Photo> photos = _photoManager.Get();  
            return View(); 
        }

        /// <summary>
        /// Get a users images to return as a gallery.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyImages()
        {
            IList<Photo> photos = _photoManager.Get(new User{UserId = CurrentUser.Id});

            var galleryModel = new GalleryModel();

            foreach (var photo in photos)
            {
                galleryModel.PhotoModels.Add(new PhotoModel
                    {
                        PhotoId = photo.PhotoId,
                        Title = photo.Title,
                        Description = photo.Description,
                        SubmittedOn = photo.CreatedOnDateUtc,
                        Tags = _tagManager.Get(photo.PhotoId)
                    });
            }

            return View(galleryModel); 
        }

        /// <summary>
        /// Gets the most recently added Image for the home page.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult HomeImage()
        {
            Photo photo = _photoManager.Get(1);

            return File(photo.PhotoData, photo.ImageType);
        }

        /// <summary>
        /// Get an image by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Image(int id)
        {
            Photo photo = _photoManager.Get(id);

            return File(photo.PhotoData, photo.ImageType);
        }

    }
}

using System.IO;
using System.Web.Mvc;
using FarmPhoto.Common.Configuration;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using System.Collections.Generic;

namespace FarmPhoto.Website.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ITagManager _tagManager;
        private readonly IConfig _config;
        private readonly IPhotoManager _photoManager;

        public GalleryController(IPhotoManager photoManager, ITagManager tagManager, IConfig config)
        {
            _photoManager = photoManager;
            _tagManager = tagManager;
            _config = config;
        }

        /// <summary>
        /// Returns all approved images as a gallery newest to oldest
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            int page = 1; 
            IList<Photo> photos = _photoManager.Get(page, _config.PhotosPerPage);  

            return View(PhotoListToGalleryModel(photos)); 
        }

        /// <summary>
        /// Get a users images to return as a gallery.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPhotos()
        {
            IList<Photo> photos = _photoManager.Get(new User{UserId = CurrentUser.Id});

            return View(PhotoListToGalleryModel(photos)); 
        }

        /// <summary>
        /// Get a users images to return as a gallery.
        /// </summary>
        /// <returns></returns>
        public ActionResult UsersPhotos(string username)
        {
            ViewBag.Username = username; 

            IList<Photo> photos = _photoManager.Get(new User { UserName = username });

            return View(PhotoListToGalleryModel(photos));
        }

        /// <summary>
        /// Gets the most recently added Image for the home page.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult HomeImage()
        {
            Photo photo = _photoManager.Get(0, false);

            return File(photo.PhotoData, photo.ImageType);
        }

        [AllowAnonymous]
        public ActionResult Photo(int? id)
        {

            if (id.HasValue)
            {
                Photo photo = _photoManager.Get(id.Value, true);

                var photoModel = new PhotoModel
                {
                    PhotoId = photo.PhotoId,
                    UserId = photo.UserId,
                    Description = photo.Description,
                    Title = photo.Title,
                    Tags = _tagManager.Get(photo.PhotoId),
                    Width = "" + photo.Width + "px",
                    Height = "" + photo.Height + "px"
                };

                photoModel.TagString = string.Join(", ", photoModel.Tags);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Photo", photoModel);
                }

                return View(photoModel);
            }

            return View("Error"); 
        }

        [HttpPost]
        public ActionResult Photo(PhotoModel photoModel)
        {
            if (Request.IsAjaxRequest())
            {
                _photoManager.Update(new Photo
                    {
                        PhotoId = photoModel.PhotoId,
                        Title =  photoModel.Title,
                        Description = photoModel.Description
                    }); 

                return PartialView("_Photo", photoModel);
            }

            return View(photoModel);
        }

        /// <summary>
        /// Get an image by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Image(int id, bool thumbnail = true)
        {
            Photo photo = _photoManager.Get(id, thumbnail);

            return File(photo.PhotoData, photo.ImageType);
        }

        [AllowAnonymous]
        public ActionResult Preview(string filename)
        {
            var uploadPath = Server.MapPath("~/App_Data/Uploads/Thumbnails");
            string uploadedFilePath = Path.Combine(uploadPath, filename);

            return File(uploadedFilePath, "image/jpeg");
        }

        private GalleryModel PhotoListToGalleryModel(IEnumerable<Photo> photos) 
        {
            var galleryModel = new GalleryModel();

            foreach (var photo in photos)
            {
                galleryModel.PhotoModels.Add(new PhotoModel
                {
                    PhotoId = photo.PhotoId,
                    UserId = photo.UserId,
                    Title = photo.Title,
                    Description = photo.Description,
                    SubmittedBy = photo.SubmittedBy,
                    SubmittedOn = photo.CreatedOnDateUtc,
                    Tags = _tagManager.Get(photo.PhotoId)
                });
            }

            return galleryModel;
        }
    }
}

using System.IO;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Website.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IConfig _config;
        private readonly ITagManager _tagManager;
        private readonly IPhotoManager _photoManager;
        private readonly IRatingManager _ratingManager;

        public GalleryController(IPhotoManager photoManager, ITagManager tagManager, IConfig config, IRatingManager ratingManager)
        {
            _config = config;
            _tagManager = tagManager;
            _photoManager = photoManager;
            _ratingManager = ratingManager;
        }

        /// <summary>
        /// Returns all approved images as a gallery newest to oldest
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            int pageing = 1;
            if (page.HasValue)
            {
                pageing = page.Value;
            }
            var from = (pageing - 1) * _config.PhotosPerPage + 1;
            var to = pageing * _config.PhotosPerPage;

            IList<Photo> photos = _photoManager.Get(from, to);
            ViewBag.Page = pageing;

            return View(PhotoListToGalleryModel(photos));
        }

        /// <summary>
        /// Get a users images to return as a gallery.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPhotos(int? page)
        {
            int pageing = 1;
            if (page.HasValue)
            {
                pageing = page.Value;
            }
            var from = (pageing - 1) * _config.PhotosPerPage + 1;
            var to = pageing * _config.PhotosPerPage;

            IList<Photo> photos = _photoManager.Get(from, to, new User { UserId = CurrentUser.Id });

            ViewBag.Page = pageing;

            return View(PhotoListToGalleryModel(photos));
        }

        /// <summary>
        /// Get a users images to return as a gallery.
        /// </summary>
        /// <returns></returns>
        public ActionResult UsersPhotos(string username, int? page)
        {
            int pageing = 1;
            if (page.HasValue)
            {
                pageing = page.Value;
            }
            var from = (pageing - 1) * _config.PhotosPerPage + 1;
            var to = pageing * _config.PhotosPerPage;

            ViewBag.Username = username;
            ViewBag.Page = pageing;
            IList<Photo> photos = _photoManager.Get(from, to, new User { UserName = username.ToLower() });

            return View(PhotoListToGalleryModel(photos));
        }

        /// <summary>
        /// Get a all photos with the following Tag.
        /// </summary>
        /// <returns></returns>
        public ActionResult Tag(string tag, int? page)
        {
            int pageing = 1;
            if (page.HasValue)
            {
                pageing = page.Value;
            }
            var from = (pageing - 1) * _config.PhotosPerPage + 1;
            var to = pageing * _config.PhotosPerPage;
            ViewBag.Tag = tag;
            ViewBag.Page = pageing;
            IList<Photo> photos = _photoManager.Get(from, to, new Tag { Description = tag });

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

            string directoryPath = Server.MapPath("~/App_Data/Uploads");

            string uploadedFilePath = Path.Combine(directoryPath, photo.FileName);

            return File(uploadedFilePath, "image/jpeg");
        }

        [AllowAnonymous]
        public ActionResult Photo(int? id)
        {
            if (id.HasValue)
            {
                Photo photo = _photoManager.Get(id.Value, true);

                var userId = Request.IsAuthenticated ? CurrentUser.Id : 0 ; 

                var rating = _ratingManager.Get(new Rating{PhotoId = id.Value, UserId = userId}); 
                var photoModel = new PhotoModel
                {
                    PhotoId = photo.PhotoId,
                    UserId = photo.UserId,
                    Description = photo.Description,
                    Title = photo.Title,
                    FileName = photo.FileName,
                    Tags = _tagManager.Get(photo.PhotoId),
                    Width = "" + photo.Width + "px",
                    Height = "" + photo.Height + "px",
                    Rating = rating
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
                        Title = photoModel.Title,
                        Description = photoModel.Description
                    });

                return PartialView("_Photo", photoModel);
            }

            return View(photoModel);
        }

        /// <summary>
        /// Get an image by id.
        /// </summary>
        /// <param name="fileName">The id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Image(string fileName, bool thumbnail = true)
        {
            string directoryPath;

            if (thumbnail)
            {
                directoryPath = Server.MapPath("~/App_Data/Uploads/Thumbnails");
            }
            else
            {
                directoryPath = Server.MapPath("~/App_Data/Uploads");
            }

            string uploadedFilePath = Path.Combine(directoryPath, fileName);

            return File(uploadedFilePath, "image/jpeg");
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
                    FileName = photo.FileName,
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

using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPhotoManager _photoManager;
        private readonly IConfig _config;
        private const bool NotApproved = false;

        public AdminController(IPhotoManager photoManager, IConfig config)
        {
            _photoManager = photoManager;
            _config = config;
        }

        public ActionResult Index()
        {
            if (CurrentUser.IsAdministrator)
            {
                var galleryModel = new GalleryModel();

                IList<Photo> photos = new List<Photo>(_photoManager.Get(1, _config.PhotosPerPage, NotApproved));

                foreach (var photo in photos)
                {
                    galleryModel.PhotoModels.Add(new PhotoModel
                        {
                            PhotoId = photo.PhotoId,
                            UserId = photo.UserId,
                            Description = photo.Description,
                            Title = photo.Title,
                            FileName = photo.FileName,
                            Approved = photo.Approved,
                            SubmittedBy = photo.SubmittedBy, 
                            SubmittedOn = photo.CreatedOnDateUtc
                        });
                }

                return View(galleryModel);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Approves the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Approve(int? id)
        {
            if (id.HasValue && CurrentUser.IsAdministrator)
            {
                const bool approved = true; 

                var success = _photoManager.Update(id.Value, approved);

                if (success > 0)
                {
                    return new JsonResult { Data = new { Success = true } };
                }
            }

            return new JsonResult { Data = new { Success = false } }; 
        }

        /// <summary>
        /// Soft Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id.HasValue && CurrentUser.IsAdministrator)
            {
                var success = _photoManager.Delete(id.Value);

                if (success > 0)
                {
                    return new JsonResult { Data = new { Success = true } };
                }
            }

            return new JsonResult { Data = new { Success = false } };
        }
    }
}

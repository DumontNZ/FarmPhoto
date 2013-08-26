using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using System.Drawing.Imaging;
using FarmPhoto.Website.Models;
using FarmPhoto.Common.Configuration;
using Ninject.Extensions.Logging;

namespace FarmPhoto.Website.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IConfig _config;
        private readonly ILogger _logger;
        private readonly ITagManager _tagManager;
        private readonly IPhotoManager _photoManager;
        private readonly IRatingManager _ratingManager;

        public SubmissionController(IPhotoManager photoManager, ILogger logger, ITagManager tagManager, IConfig config, IRatingManager ratingManager)
        {
            _logger = logger;
            _config = config;
            _tagManager = tagManager;
            _photoManager = photoManager;
            _ratingManager = ratingManager;
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
                var uploadPath = Server.MapPath("~/App_Data/Uploads");
                string uploadedFilePath = Path.Combine(uploadPath, model.FileName);
                Image img = Image.FromFile(uploadedFilePath);
                var photo = new Photo
                    {
                        Title = model.Title,
                        Description = model.Description,
                        FileName = model.FileName,
                        Width = img.Width,
                        Height = img.Height,
                        UserId = CurrentUser.Id
                    };
                var photoId = _photoManager.CreatePhoto(photo);

                _tagManager.CreateTag(model.Tags, photoId);

                return RedirectToAction("MyPhotos", "Gallery");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Update(SubmissionModel model)
        {
            if (model.UserId == CurrentUser.Id)
            {
                var photo = new Photo
                {
                    PhotoId = model.PhotoId,
                    Title = model.Title,
                    Description = model.Description,
                    UserId = CurrentUser.Id
                };
                var success = _photoManager.Update(photo);

                _tagManager.Update(model.Tags, model.PhotoId);

                return new JsonResult { Data = new { Success = true } };
            }

            return new JsonResult { Data = new { Success = false } };
        }

        [HttpPost]
        public JsonResult Rate(double score, int photoId)
        {
            try
            {
                var rating = new Rating
                {
                    Score = score,
                    PhotoId = photoId,
                    UserId = CurrentUser.Id
                };

                var newRating = _ratingManager.Submit(rating);

                return new JsonResult { Data = new { Success = true, Rating = newRating } };
            }
            catch(Exception e)
            {
                return new JsonResult { Data = new { Success = false } };
            }
        }

        [HttpPost]
        public JsonResult Delete(int photoId, int userId)
        {
            if (userId == CurrentUser.Id)
            {
                var success = _photoManager.Delete(photoId);

                return new JsonResult { Data = new { Success = true } };
            }

            return new JsonResult { Data = new { Success = false } };
        }

        [HttpPost]
        public JsonResult Upload(int? chunk, string name)
        {
            try
            {
                _logger.Info("SubmissionController, Upload, beginning Upload"); 

                HttpPostedFileBase fileUpload = Request.Files[0];

                var uploadPath = Server.MapPath("~/App_Data/Uploads");

                string uploadedFilePath = Path.Combine(uploadPath, name);

                var thumbnailUploadPath = uploadPath + "\\Thumbnails";
                string thumbnailFilePath = Path.Combine(thumbnailUploadPath, name);

                _logger.Info("SubmissionController, Upload, upload location" + thumbnailFilePath);

                if (fileUpload != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileUpload.InputStream.CopyTo(memoryStream);
                        Image image = Image.FromStream(memoryStream);
                        ScaleImage(image, 200, 200, thumbnailFilePath);
                        ScaleImage(image, 800, 800, uploadedFilePath);
                    }
                }

                return new JsonResult { Data = new { Success = true } };
            }
            catch (Exception e)
            {
                _logger.Error("SubmissionController, Upload, Error Uploading File to System :" + e.Message);

                return new JsonResult { Data = new { Success = false } };
            }
        }

        private static void ScaleImage(Image image, int maxWidth, int maxHeight, string imageLocation)
        {
            
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            newImage.Save(imageLocation, ImageFormat.Jpeg);
        }
    }
}

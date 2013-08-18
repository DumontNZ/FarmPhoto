using System;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using System.Drawing.Imaging;
using FarmPhoto.Website.Models;
using FarmPhoto.Common.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FarmPhoto.Website.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ITagManager _tagManager;
        private readonly IConfig _config;
        private readonly IPhotoManager _photoManager;

        public SubmissionController(IPhotoManager photoManager, ITagManager tagManager, IConfig config)
        {
            _photoManager = photoManager;
            _tagManager = tagManager;
            _config = config;
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

                var photo = new Photo
                    {
                        Title = model.Title,
                        Description = model.Description,
                        FileName = uploadedFilePath,
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
        public ActionResult Upload(int? chunk, string name)
        {
            try
            {
                HttpPostedFileBase fileUpload = Request.Files[0];

                if (fileUpload != null)
                {
                    var storageConnectionString = _config.StorageConnectionString;

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    SaveImage(fileUpload, blobClient, name, "thumbnails", 200, 200);

                    SaveImage(fileUpload, blobClient, name, "photos", 800, 800);

                }
                else
                {
                    throw new Exception("Shit went bad");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home"); 
                //throw new Exception(e.Message);

            }

            return View("Index");
        }

        private void SaveImage(HttpPostedFileBase fileUpload, CloudBlobClient blobClient, string filename, string containerName, int width, int height)
        {
            CloudBlobContainer thumbnailContainer = blobClient.GetContainerReference(containerName);

            CloudBlockBlob thumbnailBlob = thumbnailContainer.GetBlockBlobReference(filename); 
            Image image = Image.FromStream(fileUpload.InputStream);

            using (var thumbnail = ScaleImage(image, width, height))
            {
                using (var memoryStream = new MemoryStream())
                {
                    thumbnail.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;
                    thumbnailBlob.UploadFromStream(memoryStream);
                }
            }
        }

        private Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage; 
        }
    }
}

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
            HttpPostedFileBase fileUpload = Request.Files[0];

            //var storageConnectionString = _config.StorageConnectionString;

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer photoContainer = blobClient.GetContainerReference("photos");

            //CloudBlockBlob blockBlob = photoContainer.GetBlockBlobReference(name); 

            //blockBlob.UploadFromStream(fileUpload.InputStream);






            
            var uploadPath = Server.MapPath("~/App_Data/Uploads");
            chunk = chunk ?? 0;
            string uploadedFilePath = Path.Combine(uploadPath, name);

            var fileName = Path.GetFileName(uploadedFilePath);

            using (var fs = new FileStream(uploadedFilePath, chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            var thumbnailUploadPath = uploadPath + "\\Thumbnails";
            string thumbnailFilePath = Path.Combine(thumbnailUploadPath, name);

            Image image = Image.FromStream(System.IO.File.OpenRead(uploadedFilePath));

            ScaleImage(image, 200, 200, thumbnailFilePath);

            return View("Index");
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

            var ms = new MemoryStream();

            newImage.Save(imageLocation, ImageFormat.Jpeg);
            
            ms.Dispose();
        }
    }
}

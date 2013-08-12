using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using FarmPhoto.Common.Configuration;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

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
            var hodler = Server.MapPath("~/App_Data/Uploads/Thumbnails/");
            var encodedString = Server.HtmlEncode(hodler); 
           
            return View(new SubmissionModel { GalleryDirectory = encodedString });
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
        public ActionResult Upload(int? chunk, string name)
        {
            var fileUpload = Request.Files[0];
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

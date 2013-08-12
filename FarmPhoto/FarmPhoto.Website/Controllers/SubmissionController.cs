using System;
using System.IO;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ITagManager _tagManager;
        private readonly IPhotoManager _photoManager;

        public SubmissionController(IPhotoManager photoManager, ITagManager tagManager)
        {
            _photoManager = photoManager;
            _tagManager = tagManager;
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
                var photo = new Photo
                    {
                        Title = model.Title,
                        Description = model.Description,
                        ImageType = model.File.ContentType,
                        UserId = CurrentUser.Id
                    };
                var photoId = _photoManager.CreatePhoto(photo, model.File);

                _tagManager.CreateTag(model.Tags, photoId);

                return RedirectToAction("MyPhotos", "Gallery");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(int? chunk, string name)
        {

            string filePath = "";
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

            //Log to DB for future processing




            //var fileUpload = Request.Files[0];

            //if (fileUpload != null)
            //{
            //    var photo = new Photo
            //    {
            //        Title = new Guid().ToString(),
            //        Description = new Guid().ToString(),
            //        ImageType = fileUpload.ContentType,
            //        UserId = CurrentUser.Id
            //    };

            //    var photoId = _photoManager.CreatePhoto(photo, fileUpload);
            //}

            return View("Index");
        }
    }
}

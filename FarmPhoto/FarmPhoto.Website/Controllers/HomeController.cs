using System.Drawing;
using System.IO;
using System.Web.Mvc;
using FarmPhoto.Core;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoManager _photoManager;

        public HomeController(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetMostRecentImage()
        {

            ImageConverter converter = new ImageConverter();

            Image myImg = Image.FromFile(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\Uploads\117699.jpg");

            var holder2 = File(Path.Combine(Server.MapPath("~/App_Data/Uploads"), "117700.jpg"), "image/jpg");


            FilePathResult holder =
                File(
                    @"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\Uploads\117700.jpg",
                    "image/jpg");
            return holder2;

            // return (byte[])converter.ConvertTo(myImg, typeof(byte[]));

            //byte[] image = (byte[]) converter.ConvertTo(myImg, typeof (byte[])); 
            //return File(image, "image/jpg");
        }

    }
}

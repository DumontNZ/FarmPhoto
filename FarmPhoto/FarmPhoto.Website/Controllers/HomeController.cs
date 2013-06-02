using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoManager _photoManager;

        public HomeController()
        {
        }

        public HomeController(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        public ActionResult Index()
        {

            return View(new PictureModel()); 
        }

        

        public ActionResult GetMostRecentImage()
        {

            ImageConverter converter = new ImageConverter();

            Image myImg = Image.FromFile(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\Uploads\117699.jpg");

            FilePathResult holder =
                File(
                    @"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\Uploads\117699.jpg",
                    "image/jpg");
            return holder; 

           // return (byte[])converter.ConvertTo(myImg, typeof(byte[]));

            //byte[] image = (byte[]) converter.ConvertTo(myImg, typeof (byte[])); 
            //return File(image, "image/jpg");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

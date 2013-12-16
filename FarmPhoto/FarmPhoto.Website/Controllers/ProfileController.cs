using System.Web.Mvc;
using FarmPhoto.Core;
using FarmPhoto.Domain;
using FarmPhoto.Website.Models;

namespace FarmPhoto.Website.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserManager _userManager;

        public ProfileController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            //ProfileInformation profileInformation = _userManager.GetProfileInformation(CurrentUser.Username);

            var profileModel = new ProfileModel
                {
                    NumberOfPhotosUploaded = 4,
                    NumberOfTimesVoted = 56,
                    AverageScore = 34,
                    AverageVote = 243,
                    Username = "DumontNZ"
                };

            return View(profileModel);
        }
    }
}

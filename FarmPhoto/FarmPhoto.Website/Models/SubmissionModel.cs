using System.Web;

namespace FarmPhoto.Website.Models
{
    public class SubmissionModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set;  }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}
using System.Web;

namespace FarmPhoto.Website.Models
{
    public class SubmissionModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set;  }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Web;
using FarmPhoto.Website.Attributes;

namespace FarmPhoto.Website.Models
{
    public class SubmissionModel
    {
        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }

        public int PhotoId { get; set; }

        public int UserId { get; set; }

        [Required]
        [LocalisedDisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [LocalisedDisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [LocalisedDisplayName("Tags")]
        public string Tags { get; set; }
    }
}
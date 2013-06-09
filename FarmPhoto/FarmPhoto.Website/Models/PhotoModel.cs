using System.Collections.Generic;

namespace FarmPhoto.Website.Models
{
    public class PhotoModel
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public IList<string> Tags { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string SubmittedBy { get; set; }
    }
}
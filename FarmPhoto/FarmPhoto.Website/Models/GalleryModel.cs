using System.Collections.Generic;

namespace FarmPhoto.Website.Models
{
    public class GalleryModel
    {
        public GalleryModel()
        {
            PhotoModels = new List<PhotoModel>();
        }

        public IList<PhotoModel> PhotoModels { get; set; }
    }
}
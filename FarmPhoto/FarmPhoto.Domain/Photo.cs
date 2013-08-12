using System;

namespace FarmPhoto.Domain
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public byte[] PhotoData { get; set; }
        public byte[] ThumbnailData { get; set; }
        public string ImageType { get; set; }
        public int FileSize { get; set; }
        public int ThumbnailSize { get; set; }
        public int UserId { get; set; }
        public bool Approved { get; set; }
        public string SubmittedBy { get; set; }
        public int Width{ get; set; }
        public int Height { get; set; }
        public DateTime CreatedOnDateUtc { get; set; }
        public DateTime DeletedOnDateUtc { get; set; }
    }
}

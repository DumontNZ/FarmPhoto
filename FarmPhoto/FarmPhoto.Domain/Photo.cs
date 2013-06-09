namespace FarmPhoto.Domain
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] PhotoData { get; set; }
        public byte[] ThumbnailData { get; set; }
        public ImageType ImageType { get; set; }
        public int FileSize { get; set; }
        public int ThumbnailSize { get; set; }
        public int UserId { get; set; }
    }
}

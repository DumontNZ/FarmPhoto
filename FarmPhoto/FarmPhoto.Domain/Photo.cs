namespace FarmPhoto.Domain
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public byte[] PhotoData { get; set; }
        public ImageType ImageType { get; set; }
        public int FileSize { get; set; }
    }
}

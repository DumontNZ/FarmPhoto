namespace FarmPhoto.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double PhotoRating { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
    }
}

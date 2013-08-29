namespace FarmPhoto.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double Score { get; set; }
        public int Count { get; set;  }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public bool UserHasVoted { get; set; }
    }
}

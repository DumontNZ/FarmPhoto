namespace FarmPhoto.Website.Models
{
    public class ProfileModel
    {
        public int NumberOfPhotosUploaded { get; set; }

        public double AverageScore { get; set; }

        public int NumberOfTimesVoted { get; set; }

        public double AverageVote { get; set; }

        public string Username { get; set; }
    }
}
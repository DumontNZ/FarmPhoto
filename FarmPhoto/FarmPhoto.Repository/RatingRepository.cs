using System;
using FarmPhoto.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IConfig _config;
        private readonly string _connectionString; 

        public RatingRepository(IConfig config)
        {
            _config = config;
            _connectionString = _config.SqlConnectionString;
        }

        public Rating Get(Rating rating)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText =
                        "select RatingId, Score, PhotoId, UserId from Rating where PhotoId = @PhotoId and DeletedOnDateUTC is null"
                };

                command.Parameters.AddWithValue("PhotoId", rating.PhotoId);

                var ratings = new List<Rating>();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var newRating = new Rating
                        {
                            RatingId = Convert.ToInt32(dataReader["RatingId"]),
                            PhotoId = Convert.ToInt32(dataReader["PhotoId"]),
                            Score = Convert.ToDouble(dataReader["Score"]),
                            UserId = Convert.ToInt32(dataReader["UserId"])
                        };

                        ratings.Add(newRating);
                    }
                }

                double score = 0;
                var photoRating = new Rating(); 
                if (ratings.Count > 0)
                {
                    foreach (var rate in ratings)
                    {
                        score += rate.Score;
                        if (rate.UserId == rating.UserId)
                        {
                            photoRating.UserHasVoted = true;
                        }
                    }

                    photoRating.Score = score/ratings.Count; 
                    return photoRating;
                }

                return photoRating; 
            }
        }

        public Rating Submit(Rating rating)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText =
                        "Insert into Rating(Score, PhotoId, UserId, CreatedOnDateUTC) " +
                        "values(@Score, @PhotoId, @UserId, @CreatedOnDateUTC)" 
                };

                command.Parameters.AddWithValue("@Score", rating.Score);
                command.Parameters.AddWithValue("@PhotoId", rating.PhotoId);
                command.Parameters.AddWithValue("@UserId", rating.UserId);
                command.Parameters.AddWithValue("@CreatedOnDateUTC", DateTime.UtcNow);

                command.ExecuteNonQuery();
            }
            return Get(rating); 
        }
    }
}

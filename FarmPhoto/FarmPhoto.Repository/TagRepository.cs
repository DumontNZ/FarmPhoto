using System;
using FarmPhoto.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly IConfig _config;
        private readonly string _connectionString;

        public TagRepository(IConfig config)
        {
            _config = config;
            _connectionString = _config.SqlConnectionString;
        }

        /// <summary>
        /// Creates the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public int Create(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "Insert into Tag(Description, PhotoId, CreatedOnDateUTC) " +
                        "values(@Description, @PhotoId, @CreatedOnDateUTC); " +
                        "Select Cast(scope_identity() AS int)"
                };

                command.Parameters.AddWithValue("@PhotoId", tag.PhotoId);
                command.Parameters.AddWithValue("@Description", tag.Description);
                command.Parameters.AddWithValue("@CreatedOnDateUTC", DateTime.UtcNow);

                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets all the tags associated with a photo
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public IList<Tag> Get(int photoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText =
                        "select TagId, Description, PhotoId from Tag where PhotoId = @PhotoId"
                };

                command.Parameters.AddWithValue("PhotoId", photoId);

                var photoTags = new List<Tag>();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var photo = new Tag
                        {
                            TagId = Convert.ToInt32(dataReader["TagId"]),
                            PhotoId = Convert.ToInt32(dataReader["PhotoId"]),
                            Description = dataReader["Description"].ToString()
                        };

                        photoTags.Add(photo);
                    }
                }

                return photoTags;
            }
        }

        public void Delete(int photoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "delete from Tag where PhotoId = @PhotoId"
                    };

                command.Parameters.AddWithValue("PhotoId", photoId);

                command.ExecuteNonQuery();
            }
        }
    }
}

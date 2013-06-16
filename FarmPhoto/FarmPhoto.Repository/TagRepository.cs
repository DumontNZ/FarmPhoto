using FarmPhoto.Domain;
using MySql.Data.MySqlClient;
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
            var mySqlCommand = new MySqlCommand();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                const string sql = "Insert into tag(description, photoid) values(@Description, @PhotoId)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@Description", tag.Description);
                mySqlCommand.Parameters.AddWithValue("@PhotoId", tag.PhotoId);

                mySqlCommand.ExecuteNonQuery();
                return (int)mySqlCommand.LastInsertedId;
            }
        }

        /// <summary>
        /// Gets all the tags associated with a photo
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public IList<Tag> Get(int photoId)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand
                    {
                        Connection = sqlConnection,
                        CommandText =
                            "select tagid, desciption, photoid, from tag where photoId = @PhotoId"
                    };

                mySqlCommand.Parameters.AddWithValue("PhotoId", photoId);

                var photoTags = new List<Tag>();

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var photo = new Tag
                        {
                            TagId =  dataReader.GetInt32("tagid"),
                            PhotoId = dataReader.GetInt32("photoid"),
                            Description = dataReader.GetString("description"),
                        };

                        photoTags.Add(photo);
                    }
                }

                return photoTags;
            }
        }
    }
}

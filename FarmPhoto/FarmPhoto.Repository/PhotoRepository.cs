using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FarmPhoto.Domain;
using MySql.Data.MySqlClient;

namespace FarmPhoto.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly string _connectionString;

        public PhotoRepository()
        {
            // _logger = logger;
            _connectionString =
                System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MySqlConnectionString"]
                    .ConnectionString;
        }

        /// <summary>
        /// Creates the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public int Create(Photo photo)
        {
            var mySqlCommand = new MySqlCommand();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string sql = "Insert into photo(title, photodata, filesize) values(@Title, @PhotoData, @FileSize)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@Title", photo.Title);
                mySqlCommand.Parameters.AddWithValue("@PhotoData", photo.PhotoData);
                mySqlCommand.Parameters.AddWithValue("@FileSize", photo.FileSize);

                var holder = mySqlCommand.ExecuteNonQuery();

                return holder;
            }
        }

        /// <summary>
        /// Gets all photos that have been approved.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the specified photo by id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public Photo Get(int photoId)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();
                Photo photo = sqlConnection.Query<Photo>
                    ("Select photoid, title, photodata, filesize from Photo where photoid = @PhotoId", new Photo { PhotoId = photoId }).FirstOrDefault();

                return photo;
            }
        }

        /// <summary>
        /// Gets all the users photos.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}

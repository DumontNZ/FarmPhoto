using System;
using System.Collections.Generic;
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

                const string sql = "Insert into photo(title, description, photodata, filesize, imagetype, userid) values(@Title, @Description, @PhotoData, @FileSize, @ImageType,  @UserId)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@Title", photo.Title);
                mySqlCommand.Parameters.AddWithValue("@Description", photo.Description);
                mySqlCommand.Parameters.AddWithValue("@PhotoData", photo.PhotoData);
                mySqlCommand.Parameters.AddWithValue("@FileSize", photo.FileSize);
                mySqlCommand.Parameters.AddWithValue("@ImageType", photo.ImageType);
                mySqlCommand.Parameters.AddWithValue("@UserId", photo.UserId);

                mySqlCommand.ExecuteNonQuery();
                //@string returnNewUser = "Select from photo "
                return (int)mySqlCommand.LastInsertedId;
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
        /// Gets the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public Photo Get(Photo photo)
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

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select * from photo where photoid = @PhotoId" };

                mySqlCommand.Parameters.AddWithValue("PhotoId", photoId);

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();

                var returnedPhoto = new Photo();

                while (dataReader.Read())
                {
                    returnedPhoto.Title = dataReader.GetString("title");
                }

                return returnedPhoto;
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

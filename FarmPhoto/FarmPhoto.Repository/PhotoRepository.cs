using System;
using FarmPhoto.Domain;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfig _config;
        private readonly string _connectionString;

        public PhotoRepository(IConfig config)
        {
            _config = config;
            _connectionString = _config.SqlConnectionString;
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

                const string sql =
                    "Insert into photo(title, description, photodata, thumbnaildata, filesize, thumbnailsize, imagetype, approved, userid) values(@Title, @Description, @PhotoData, @ThumbnailData, @FileSize, @ThumbnailSize, @ImageType, @Approved, @UserId)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@Title", photo.Title);
                mySqlCommand.Parameters.AddWithValue("@Description", photo.Description);
                mySqlCommand.Parameters.AddWithValue("@PhotoData", photo.PhotoData);
                mySqlCommand.Parameters.AddWithValue("@ThumbnailData", photo.ThumbnailData);
                mySqlCommand.Parameters.AddWithValue("@FileSize", photo.FileSize);
                mySqlCommand.Parameters.AddWithValue("@ThumbnailSize", photo.ThumbnailSize);
                mySqlCommand.Parameters.AddWithValue("@ImageType", photo.ImageType);
                mySqlCommand.Parameters.AddWithValue("@Approved", true);
                mySqlCommand.Parameters.AddWithValue("@UserId", photo.UserId);

                mySqlCommand.ExecuteNonQuery();

                return (int)mySqlCommand.LastInsertedId;
            }
        }

        /// <summary>
        /// Gets all photos that have been approved.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get(int numberReturned, int page)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select photoid, title, description, userid from photo where approved = true order by createdOnDateUTC desc limit @NumberReturned" };
                mySqlCommand.Parameters.AddWithValue("NumberReturned", numberReturned);
                
                var usersPhotos = new List<Photo>();

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var photo = new Photo
                        {
                            PhotoId = dataReader.GetInt32("photoid"),
                            Title = dataReader.GetString("title"),
                            Description = dataReader.GetString("description"),
                            UserId = dataReader.GetInt32("userid")
                        };

                        usersPhotos.Add(photo);
                    }

                }

                return usersPhotos;
            }
        }

        /// <summary>
        /// Gets the specified photo by id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="thumbnail">The thumbnail.</param>
        /// <returns></returns>
        public Photo Get(int photoId, bool thumbnail)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand
                    {
                        Connection = sqlConnection

                    };
                if (photoId == 0)
                {
                    mySqlCommand.CommandText = "select photoid, title, description, photodata, imagetype, filesize, userid, createdondateutc from photo where approved = true order by createdOnDateUTC desc limit 1";
                }
                else
                {
                    if (thumbnail)
                    {
                        mySqlCommand.CommandText =
                       "select photoid, title, description, thumbnaildata, imagetype, thumbnailsize, userid, createdondateutc from photo where photoid = @PhotoId";
                    }
                    else
                    {
                        mySqlCommand.CommandText =
                        "select photoid, title, description, photodata, imagetype, filesize, userid, createdondateutc from photo where photoid = @PhotoId";
                    }

                    mySqlCommand.Parameters.AddWithValue("PhotoId", photoId);
                }

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();

                var returnedPhoto = new Photo();

                while (dataReader.Read())
                {
                    byte[] bytearray;
                    int fileSize;
                    if (thumbnail)
                    {
                        fileSize = dataReader.GetInt32("thumbnailsize");
                        bytearray = new byte[fileSize];
                        dataReader.GetBytes(3, 0, bytearray, 0, fileSize);
                    }
                    else
                    {
                        fileSize = dataReader.GetInt32("filesize");
                        bytearray = new byte[fileSize];
                        dataReader.GetBytes(3, 0, bytearray, 0, fileSize);
                    }

                    returnedPhoto.PhotoId = dataReader.GetInt32("photoid");
                    returnedPhoto.Title = dataReader.GetString("title");
                    returnedPhoto.PhotoData = bytearray;
                    returnedPhoto.FileSize = fileSize;
                    returnedPhoto.Description = dataReader.GetString("description");
                    returnedPhoto.ImageType = dataReader.GetString("imagetype");
                    returnedPhoto.UserId = dataReader.GetInt32("userid");
                    returnedPhoto.CreatedOnDateUtc = (DateTime)dataReader.GetMySqlDateTime("createdondateutc");
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
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select photoid, title, description, userid from photo where userid = @UserId order by createdOnDateUTC desc" };

                mySqlCommand.Parameters.AddWithValue("UserId", user.UserId);

                var usersPhotos = new List<Photo>();

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var photo = new Photo
                        {
                            PhotoId = dataReader.GetInt32("photoid"),
                            Title = dataReader.GetString("title"),
                            Description = dataReader.GetString("description"),
                            UserId = dataReader.GetInt32("userid")
                        };

                        usersPhotos.Add(photo);
                    }
                }

                return usersPhotos;
            }
        }
    }
}

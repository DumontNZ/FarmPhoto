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
                    "Insert into photo(title, description, photodata, thumbnaildata, filesize, thumbnailsize, imagetype, approved, width, height, userid) values(@Title, @Description, @PhotoData, @ThumbnailData, @FileSize, @ThumbnailSize, @ImageType, @Approved, @Width, @Height, @UserId)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@Title", photo.Title);
                mySqlCommand.Parameters.AddWithValue("@Description", photo.Description);
                mySqlCommand.Parameters.AddWithValue("@PhotoData", photo.PhotoData);
                mySqlCommand.Parameters.AddWithValue("@ThumbnailData", photo.ThumbnailData);
                mySqlCommand.Parameters.AddWithValue("@FileSize", photo.FileSize);
                mySqlCommand.Parameters.AddWithValue("@ThumbnailSize", photo.ThumbnailSize);
                mySqlCommand.Parameters.AddWithValue("@ImageType", photo.ImageType);
                mySqlCommand.Parameters.AddWithValue("@Approved", false);
                mySqlCommand.Parameters.AddWithValue("@Width", photo.Width);
                mySqlCommand.Parameters.AddWithValue("@Height", photo.Height); 
                mySqlCommand.Parameters.AddWithValue("@UserId", photo.UserId);

                mySqlCommand.ExecuteNonQuery();

                return (int)mySqlCommand.LastInsertedId;
            }
        }

        /// <summary>
        /// Gets all photos that have been approved if its for gallery otherwise gets unapproved if adminscreen.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="numberReturned">The number returned.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get(int page, int numberReturned, bool approved)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, 
                                                      CommandText = "select p.photoid, p.title, p.description, p.userid, p.approved, p.createdondateutc, u.username, p.width, p.height " +
                                                                     "from photo as p " +
                                                                     "inner join user as u on p.userid = u.userid " +
                                                                     "where approved = @Approved AND deletedondateutc is null " +
                                                                     "order by createdOnDateUTC " +
                                                                     "desc limit @NumberReturned"
                                                    };
                mySqlCommand.Parameters.AddWithValue("NumberReturned", numberReturned);
                mySqlCommand.Parameters.AddWithValue("Approved", approved); 

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
                            UserId = dataReader.GetInt32("userid"),
                            Approved = dataReader.GetBoolean("approved"), 
                            Width = dataReader.GetInt32("width"),
                            Height = dataReader.GetInt32("height"),
                            SubmittedBy = dataReader.GetString("username"),
                            CreatedOnDateUtc = (DateTime)dataReader.GetMySqlDateTime("createdondateutc")
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
                    mySqlCommand.CommandText = "select p.photoid, p.title, p.description, p.photodata, p.imagetype, p.filesize, p.userid, p.createdondateutc, u.username, p.width, p.height " +
                                               "from photo as p " +
                                               "inner join user as u on p.userid = u.userid " +
                                               "where approved = true AND deletedondateutc is null " +
                                               "order by createdOnDateUTC desc " +
                                               "limit 1";
                }
                else
                {
                    if (thumbnail)
                    {
                        mySqlCommand.CommandText =
                       "select p.photoid, p.title, p.description, p.thumbnaildata, p.imagetype, p.thumbnailsize, p.userid, p.createdondateutc, u.username, p.width, p.height " +
                       "from photo as p " +
                       "inner join user as u on p.userid = u.userid " +
                       "where photoid = @PhotoId AND p.deletedondateutc is null";
                    }
                    else
                    {
                        mySqlCommand.CommandText =
                        "select p.photoid, p.title, p.description, p.photodata, p.imagetype, p.filesize, p.userid, p.createdondateutc, u.username, p.width, p.height " +
                        "from photo as p " +
                        "inner join user as u on p.userid = u.userid " +
                        "where photoid = @PhotoId AND p.deletedondateutc is null";
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
                    returnedPhoto.UserId = dataReader.GetInt32("userid");
                    returnedPhoto.Description = dataReader.GetString("description");
                    returnedPhoto.ImageType = dataReader.GetString("imagetype");
                    returnedPhoto.Width = dataReader.GetInt32("width");
                    returnedPhoto.Height = dataReader.GetInt32("height"); 
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

                var mySqlCommand = new MySqlCommand
                    {
                        Connection = sqlConnection
                    };

                if (user.UserName != null)
                {
                    mySqlCommand.CommandText = "select p.photoid, p.title, p.description, p.userid, u.username, p.width, p.height " +
                                               "from photo as p " +
                                               "inner join user as u on p.userid = u.userid " +
                                               "where username = @Username and deletedondateutc is null " +
                                               "order by p.createdOnDateUTC desc";
                    

                    mySqlCommand.Parameters.AddWithValue("Username", user.UserName);
                }else
                {
                    mySqlCommand.CommandText = "select p.photoid, p.title, p.description, p.userid, u.username, p.width, p.height " +
                                               "from photo as p " +
                                               "inner join user as u on p.userid = u.userid " +
                                               "where p.userid = @UserId and deletedondateutc is null " +
                                               "order by p.createdOnDateUTC desc";

                    mySqlCommand.Parameters.AddWithValue("UserId", user.UserId);
                }

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
                            UserId = dataReader.GetInt32("userid"),
                            Width = dataReader.GetInt32("width"),
                            Height = dataReader.GetInt32("height"),
                            SubmittedBy = dataReader.GetString("username")
                        };

                        usersPhotos.Add(photo);
                    }
                }

                return usersPhotos;
            }
        }

        /// <summary>
        /// Updates the specified photos details.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Update(Photo photo)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "update photo set title = @Title, description = @Description where photoid = @PhotoId" };

                mySqlCommand.Parameters.AddWithValue("PhotoId", photo.PhotoId);
                mySqlCommand.Parameters.AddWithValue("Title", photo.Title);
                mySqlCommand.Parameters.AddWithValue("Description", photo.Description);

                return mySqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates the specified photo to approved.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        public int Update(int id, bool approved)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "update photo set approved = @Approved where photoid = @PhotoId" };

                mySqlCommand.Parameters.AddWithValue("PhotoId", id);
                mySqlCommand.Parameters.AddWithValue("Approved", approved);

                return mySqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Soft Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "update photo set deletedondateutc = @DeletedOnDateUtc where photoid = @PhotoId" };

                mySqlCommand.Parameters.AddWithValue("PhotoId", id);
                var holder = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); 
                mySqlCommand.Parameters.AddWithValue("DeletedOnDateUtc", holder);
                var holder2 = mySqlCommand.ExecuteNonQuery();
                return holder2;
            }
        }
    }
}

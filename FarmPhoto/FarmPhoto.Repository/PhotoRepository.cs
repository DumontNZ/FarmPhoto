﻿using System;
using FarmPhoto.Domain;
using System.Data.SqlClient;
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText =
                            "Insert into photo(Title, Description, Approved, Width, Height, UserId, FileName, CreatedOnDateUTC) " +
                            "values(@Title, @Description, @Approved, @Width, @Height, @UserId, @FileName, @CreatedOnDateUTC); " +
                            "Select Cast(scope_identity() AS int)"
                    };

                command.Parameters.AddWithValue("@Title", photo.Title);
                command.Parameters.AddWithValue("@Description", photo.Description);
                command.Parameters.AddWithValue("@Approved", false);
                command.Parameters.AddWithValue("@Width", photo.Width);
                command.Parameters.AddWithValue("@Height", photo.Height);
                command.Parameters.AddWithValue("@UserId", photo.UserId);
                command.Parameters.AddWithValue("@FileName", photo.FileName);
                command.Parameters.AddWithValue("@CreatedOnDateUTC", DateTime.UtcNow);

                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets all photos that have been approved if its for gallery otherwise gets unapproved if adminscreen.
        /// </summary>
        /// <param name="from">Starting Image</param>
        /// <param name="to">Last Image</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get(int from, int to, bool approved)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT * " +
                             "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY p.ApprovedOnDateUTC desc ) AS ResultNumber, " +
                             "p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.Approved, p.CreatedOnDateUTC, u.DisplayName, p.Width, p.Height " +
                             "FROM Photo as p " +
                             "inner join Users as u on p.Userid = u.UserId " +
                             "WHERE Approved = @Approved AND p.DeletedOnDateUTC is null " +
                             ") AS RowConstrainedResult " +
                             "WHERE ResultNumber >= @From " +
                             "AND ResultNumber <= @To " +
                             "ORDER BY ResultNumber "
                };

                command.Parameters.AddWithValue("Approved", approved);
                command.Parameters.AddWithValue("From", from);
                command.Parameters.AddWithValue("To", to);

                return ProcessResults(command); 
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection
                };
                if (photoId == 0)
                {
                    command.CommandText = "select top 1 p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.CreatedOnDateUTC, u.Username, p.Width, p.Height " +
                                               "from Photo as p " +
                                               "inner join Users as u on p.UserId = u.UserId " +
                                               "where Approved = 'true' AND p.DeletedOnDateUTC is null " +
                                               "order by ApprovedOnDateUTC desc";
                }
                else
                {
                    command.CommandText =
                       "select p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.CreatedOnDateUTC, u.Username, p.Width, p.Height " +
                       "from Photo as p " +
                       "inner join Users as u on p.UserId = u.UserId " +
                       "where photoid = @PhotoId AND p.DeletedOnDateUTC is null";


                    command.Parameters.AddWithValue("PhotoId", photoId);
                }

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    var returnedPhoto = new Photo();

                    while (dataReader.Read())
                    {
                        returnedPhoto.PhotoId = Convert.ToInt32(dataReader["PhotoId"]);
                        returnedPhoto.Title = dataReader["Title"].ToString();
                        returnedPhoto.UserId = Convert.ToInt32(dataReader["UserId"]);
                        returnedPhoto.Description = dataReader["Description"].ToString();
                        returnedPhoto.FileName = dataReader["FileName"].ToString();
                        returnedPhoto.Width = Convert.ToInt32(dataReader["Width"]);
                        returnedPhoto.Height = Convert.ToInt32(dataReader["Height"]);
                        returnedPhoto.CreatedOnDateUtc = (DateTime)dataReader["CreatedOnDateUTC"];
                    }

                    return returnedPhoto;
                }
            }
        }

        /// <summary>
        /// Gets all the users photos.
        /// </summary>
        /// <param name="from">Starting Photo.</param>
        /// <param name="to">Ending Photo.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Photo> Get(int from, int to, User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection
                };

                if (user.UserName != null)
                {
                    command.CommandText = "SELECT * " +
                                          "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY p.ApprovedOnDateUTC desc) AS ResultNumber, " +
                                          "p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.Approved, p.CreatedOnDateUTC, u.DisplayName, p.Width, p.Height " +
                                          "FROM Photo as p " +
                                          "inner join Users as u on p.Userid = u.UserId " +
                                          "WHERE Username = @Username AND Approved = 'true' AND p.DeletedOnDateUTC is null " +
                                          ") AS RowConstrainedResult " +
                                          "WHERE ResultNumber >= @From " +
                                          "AND ResultNumber <= @To " +
                                          "ORDER BY ResultNumber ";

                    command.Parameters.AddWithValue("Username", user.UserName);
                }
                else
                {
                    command.CommandText = "SELECT * " +
                                          "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY p.CreatedOnDateUTC desc) AS ResultNumber, " +
                                          "p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.Approved, p.CreatedOnDateUTC, u.DisplayName, p.Width, p.Height " +
                                          "FROM Photo as p " +
                                          "inner join Users as u on p.Userid = u.UserId " +
                                          "WHERE p.UserId = @UserId AND p.DeletedOnDateUTC is null " +
                                          ") AS RowConstrainedResult " +
                                          "WHERE ResultNumber >= @From " +
                                          "AND ResultNumber <= @To " +
                                          "ORDER BY ResultNumber ";

                    command.Parameters.AddWithValue("UserId", user.UserId);
                }
                command.Parameters.AddWithValue("From", from);
                command.Parameters.AddWithValue("To", to);


                return ProcessResults(command); 
            }
        }

        /// <summary>
        /// Gets all the photos that have been tagged with tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="from">Photos from</param>
        /// <param name="to">Photos to</param>
        /// <returns></returns>
        public IList<Photo> Get(int from, int to, Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText =
                        "SELECT * " +
                        "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY p.ApprovedOnDateUTC desc ) AS ResultNumber, " +
                        "p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.Approved, p.CreatedOnDateUTC, u.DisplayName, p.Width, p.Height " +
                        "FROM Photo as p " +
                        "inner join Users as u on p.UserId = u.UserId " +
                        "inner join Tag as t on p.PhotoId = t.PhotoId " +
                        "WHERE t.Description = @Description AND Approved = 'true' AND p.DeletedOnDateUTC is null " +
                        ") AS RowConstrainedResult " +
                        "WHERE ResultNumber >= @From " +
                        "AND ResultNumber <= @To " +
                        "ORDER BY ResultNumber "
                };

                command.Parameters.AddWithValue("Description", tag.Description);
                command.Parameters.AddWithValue("From", from);
                command.Parameters.AddWithValue("To", to);

                return ProcessResults(command); 
            }
        }

        public IList<Photo> Search(int from, int to, string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "select * from ( " + 
                                      "select PhotoId, Title, Description, FileName, UserId, Approved, CreatedOnDateUTC, DisplayName, Width, Height, " +
                                      "ROW_NUMBER() OVER (ORDER BY ApprovedOnDateUTC desc) as ResultNumber " +
                                      "from ( SELECT " +
                                      "p.PhotoId, p.Title, p.Description, p.FileName, p.UserId, p.Approved, p.CreatedOnDateUTC, u.DisplayName, p.Width, p.Height, p.ApprovedOnDateUTC, " +
                                      "ROW_NUMBER() over (Partition by p.PhotoId Order by p.PhotoId) as ResultInstance " +
                                      "FROM Photo as p " +
                                      "inner join Users as u on u.UserId = p.UserId " +
                                      "inner join Tag as t on t.PhotoId = p.PhotoId " +
                                      "where (p.Description like @SearchQuery " +
                                      "or Title like @SearchQuery  " +
                                      "or u.Username like @SearchQuery  " +
                                      "or t.Description like @SearchQuery) " +
                                      "and p.DeletedOnDateUTC is null and Approved = 'true') as SubQuery " +
                                      "where SubQuery.ResultInstance = 1) as b " +
                                      "where ResultNumber >= @From and ResultNumber <= @To " +
                                      "order by ResultNumber"
                    };

                command.Parameters.AddWithValue("SearchQuery", "%" + query + "%");
                command.Parameters.AddWithValue("From", from);
                command.Parameters.AddWithValue("To", to);

                return ProcessResults(command); 
            }
        }

        private IList<Photo> ProcessResults(SqlCommand command)
        {
            var searchPhotos = new List<Photo>();

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var photo = new Photo
                        {
                            PhotoId = Convert.ToInt32(dataReader["PhotoId"]),
                            Title = dataReader["Title"].ToString(),
                            Description = dataReader["Description"].ToString(),
                            FileName = dataReader["FileName"].ToString(),
                            UserId = Convert.ToInt32(dataReader["UserId"]),
                            Width = Convert.ToInt32(dataReader["Width"]),
                            Height = Convert.ToInt32(dataReader["Height"]),
                            SubmittedBy = dataReader["DisplayName"].ToString(),
                            CreatedOnDateUtc = (DateTime)dataReader["CreatedOnDateUTC"]
                        };

                    searchPhotos.Add(photo);
                }
            }
            return searchPhotos;
        }

        /// <summary>
        /// Updates the specified photos details.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Update(Photo photo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "update Photo set Title = @Title, Description = @Description  " +
                        "where PhotoId = @PhotoId"
                };

                command.Parameters.AddWithValue("PhotoId", photo.PhotoId);
                command.Parameters.AddWithValue("Title", photo.Title);
                command.Parameters.AddWithValue("Description", photo.Description);

                return command.ExecuteNonQuery();
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "update photo set approved = @Approved, ApprovedOnDateUTC = @ApprovedOnDateUTC " +
                        "where photoid = @PhotoId"
                };

                command.Parameters.AddWithValue("PhotoId", id);
                command.Parameters.AddWithValue("Approved", approved);
                command.Parameters.AddWithValue("ApprovedOnDateUTC", DateTime.UtcNow);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Soft Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "update Photo set DeletedOnDateUTC = @DeletedOnDateUtc " +
                        "where PhotoId = @PhotoId"
                };

                command.Parameters.AddWithValue("PhotoId", id);
                command.Parameters.AddWithValue("DeletedOnDateUtc", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                return command.ExecuteNonQuery();
            }
        }
    }
}

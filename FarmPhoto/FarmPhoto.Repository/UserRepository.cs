using System;
using FarmPhoto.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfig _config;
        private readonly string _connectionString;

        public UserRepository(IConfig config)
        {
            _config = config;
            // _logger = logger;
            _connectionString = _config.SqlConnectionString;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int Create(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText =
                            "Insert into Users(FirstName, Surname, Username, Email, Password, PasswordSalt, CreatedOnDateUTC) " +
                            "values(@FirstName, @Surname, @Username, @Email, @Password, @PasswordSalt, @CreatedOnDateUTC); " +
                            "Select Cast(scope_identity() AS int)"
                    };

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@Surname", user.Surname);
                command.Parameters.AddWithValue("@Username", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
                command.Parameters.AddWithValue("@CreatedOnDateUTC", DateTime.UtcNow);
                //command.Parameters.AddWithValue("@Country", user.Country);

                return (int)command.ExecuteScalar(); 
            }
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Get(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand { Connection = connection, CommandText = "select Password, Passwordsalt, UserId, FirstName, Surname, Username from Users where UserId = @UserId" };

                command.Parameters.AddWithValue("UserId", userId);

                SqlDataReader dataReader = command.ExecuteReader();

                var returnedUser = new User();

                while (dataReader.Read())
                {
                    returnedUser.Password = dataReader["Password"].ToString();
                    returnedUser.PasswordSalt = dataReader["PasswordSalt"].ToString();
                    returnedUser.UserId = Convert.ToInt32(dataReader["Userid"]);
                    returnedUser.FirstName = dataReader["FirstName"].ToString();
                    returnedUser.Surname = dataReader["Surname"].ToString();
                    returnedUser.UserName = dataReader["Username"].ToString();
                }

                return returnedUser;
            }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user to get.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Get(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand { Connection = connection, CommandText = "select Password, PasswordSalt, UserId, FirstName, Surname, Username from Users where Username = @UserName" };

                command.Parameters.AddWithValue("UserName", user.UserName);

                SqlDataReader dataReader = command.ExecuteReader();

                var returnedUser = new User();

                while (dataReader.Read())
                {
                    returnedUser.Password = dataReader["Password"].ToString();
                    returnedUser.PasswordSalt = dataReader["PasswordSalt"].ToString();
                    returnedUser.UserId = Convert.ToInt32(dataReader["Userid"]);
                    returnedUser.FirstName = dataReader["FirstName"].ToString();
                    returnedUser.Surname = dataReader["Surname"].ToString();
                    returnedUser.UserName = dataReader["Username"].ToString();
                }

                return returnedUser;
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Update(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Update(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deleteusers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}

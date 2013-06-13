using System;
using FarmPhoto.Domain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FarmPhoto.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            // _logger = logger;
            _connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int Create(User user)
        {

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user to get.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User Get(User user)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select * from user where username = @UserName" };

                mySqlCommand.Parameters.AddWithValue("UserName", user.UserName);

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();

                var returnedUser = new User();

                while (dataReader.Read())
                {
                    returnedUser.Password = dataReader.GetString("password");
                    returnedUser.PasswordSalt = dataReader.GetString("passwordsalt");
                    returnedUser.UserId = dataReader.GetInt32("userid");
                    returnedUser.FirstName = dataReader.GetString("firstname");
                    returnedUser.Surname = dataReader.GetString("surname"); 
                    returnedUser.UserName = dataReader.GetString("username");
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

using System;
using FarmPhoto.Common.Configuration;
using FarmPhoto.Domain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

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
            var mySqlCommand = new MySqlCommand();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                const string sql =
                    "Insert into user(firstname, surname, username, email, password, passwordsalt, country) values(@FirstName, @Surname, @Username, @Email, @Password, @PasswordSalt, @Country)";

                mySqlCommand.Connection = sqlConnection;
                mySqlCommand.CommandText = sql;
                mySqlCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                mySqlCommand.Parameters.AddWithValue("@Surname", user.Surname);
                mySqlCommand.Parameters.AddWithValue("@Username", user.UserName);
                mySqlCommand.Parameters.AddWithValue("@Email", user.Email);
                mySqlCommand.Parameters.AddWithValue("@Password", user.Password);
                mySqlCommand.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
                mySqlCommand.Parameters.AddWithValue("@Country", user.Country);

                mySqlCommand.ExecuteNonQuery();

                return (int)mySqlCommand.LastInsertedId;
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
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select password, passwordsalt, userid, firstname, surname, username from user where userid = @UserId" };

                mySqlCommand.Parameters.AddWithValue("UserId", userId);

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

                var mySqlCommand = new MySqlCommand { Connection = sqlConnection, CommandText = "select password, passwordsalt, userid, firstname, surname, username from user where username = @UserName" };

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

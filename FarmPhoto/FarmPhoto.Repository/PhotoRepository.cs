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
            _connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        public int Create(Photo photo)
        {
            var conn = new MySqlConnection();
            var cmd = new MySqlCommand();

            conn.ConnectionString = _connectionString;

            conn.Open();
            string sql = "Insert into photo(title, photodata, filesize) values(@Title, @PhotoData, @FileSize)";

            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@Title", photo.Title);
            cmd.Parameters.AddWithValue("@PhotoData", photo.PhotoData);
            cmd.Parameters.AddWithValue("@FileSize", photo.FileSize);

            var holder = cmd.ExecuteNonQuery();

            conn.Close();

            return holder;

            //var mySqlCommand = new MySqlCommand();


            //using (var sqlConnection = new MySqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();
            //    mySqlCommand.Connection = sqlConnection;
            //    mySqlCommand.CommandText = "Insert into photo(title, photodata, filesize) values(@Title, @PhotoData, @FileSize)";
            //    mySqlCommand.Parameters.AddWithValue("@Title", photo.Title);
            //    mySqlCommand.Parameters.AddWithValue("@FileSize", photo.FileSize);
            //    mySqlCommand.Parameters.AddWithValue("@PhotoData", photo.PhotoData);

            //    int somthing =  mySqlCommand.ExecuteNonQuery();

            //    return somthing;
            //}
        }
    }
}

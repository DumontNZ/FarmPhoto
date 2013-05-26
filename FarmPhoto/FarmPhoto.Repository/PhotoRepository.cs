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
            _connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        public int CreatePhoto(Photo photo)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var succcessful = sqlConnection.Execute
                   ("Insert into photo(title) values(@Title)", photo);

                return succcessful;
            }
        }
    }
}

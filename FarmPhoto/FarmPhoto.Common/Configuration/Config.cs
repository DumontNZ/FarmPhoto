using System;
using System.Configuration;

namespace FarmPhoto.Common.Configuration
{
    public class Config : IConfig
    {

        /// <summary>
        /// Gets the configuration values.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetConfigurationValues(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets the SQL connection string.
        /// </summary>
        /// <value>
        /// The SQL connection string.
        /// </value>
        public string SqlConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[ConfigKeys.SqlConnection].ConnectionString; }
        }

        /// <summary>
        /// Gets the year in minutes.
        /// </summary>
        /// <value>
        /// The year in minutes.
        /// </value>
        public double YearInMinutes
        {
            get { return ConvertToDouble(ConfigKeys.YearInMinutes); }
        }

        /// <summary>
        /// Gets the administrator username.
        /// </summary>
        /// <value>
        /// The administrator username.
        /// </value>
        public string AdministratorUsername
        {
            get { return GetConfigurationValues(ConfigKeys.AdministratorUsername); }
        }

        /// <summary>
        /// Gets the number of photos per page.
        /// </summary>
        /// <value>
        /// The photos per page.
        /// </value>
        public int PhotosPerPage
        {
            get { return ConvertToInt(ConfigKeys.PhotosPerPage); }
        }

        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static int ConvertToInt(string key)
        {
            string value = GetConfigurationValues(key);
            int intOut;
            if (int.TryParse(value, out intOut))
            {
                return intOut;
            }
            throw new InvalidOperationException("Key not found in Configuration file");
        }

        /// <summary>
        /// Converts string to double.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static double ConvertToDouble(string key)
        {
            string value = GetConfigurationValues(key);
            double doubleOut;
            if (double.TryParse(value, out doubleOut))
            {
                return doubleOut;
            }
            throw new InvalidOperationException("Key not found in Configuration file");
        }
    }
}

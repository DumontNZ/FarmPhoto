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

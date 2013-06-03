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
            get { return GetConfigurationValues(ConfigKeys.SqlConnection); }
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

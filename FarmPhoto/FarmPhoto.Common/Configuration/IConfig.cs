namespace FarmPhoto.Common.Configuration
{
    public interface IConfig
    {
        /// <summary>
        /// Gets the SQL connection string.
        /// </summary>
        /// <value>
        /// The SQL connection string.
        /// </value>
        string SqlConnectionString { get; }

        /// <summary>
        /// Gets the year in minutes.
        /// </summary>
        /// <value>
        /// The year in minutes.
        /// </value>
        double YearInMinutes { get; }

        /// <summary>
        /// Gets the administrator username.
        /// </summary>
        /// <value>
        /// The administrator username.
        /// </value>
        string AdministratorUsername { get; }
    }
}
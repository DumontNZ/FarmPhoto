using FarmPhoto.Common.Configuration;
using NUnit.Framework;

namespace FarmPhoto.Common.Tests
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void ConfigRetrievesValuesSuccessfully()
        {
            var config = new Config();

            var configurationKey = config.SqlConnectionString; 

            Assert.IsNotNull(configurationKey);
        }

    }
}

using System.Transactions;
using NUnit.Framework;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class TestBase
    {
        private TransactionScope _scope;

        [SetUp]
        public void Initalize()
        {
            _scope = new TransactionScope();
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }
    }
}

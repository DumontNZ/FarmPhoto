using FarmPhoto.Domain;
using NUnit.Framework;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class PhotoTests
    {
        [Test]
        public void CreatePhtoTest()
        {
            var photoRepository = new PhotoRepository();
            int successfull = photoRepository.CreatePhoto(new Photo {Title = "New Photo"});

            Assert.IsTrue(successfull == 1); 
        }

    }
}

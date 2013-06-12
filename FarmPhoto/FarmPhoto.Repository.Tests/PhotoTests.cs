using System;
using System.IO;
using NUnit.Framework;
using FarmPhoto.Domain;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class PhotoTests
    {
        private readonly PhotoRepository _photoRepository;

        public PhotoTests()
        {

            _photoRepository = new PhotoRepository(); 

        }

        [Test]
        public void CreatePhotoTest()
        {

            byte[] rawData = File.ReadAllBytes(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            FileInfo info = new FileInfo(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            int fileSize = Convert.ToInt32(info.Length);

            int successfull = _photoRepository.Create(new Photo { Title = "New Photo", Description = "Blah", ImageType = "image/jpeg", PhotoData = rawData, FileSize = fileSize, UserId = 1});

            Assert.IsTrue(successfull > 0); 
        }

        [Test]
        public void GetPhotoTest()
        {

            Photo photo = _photoRepository.Get(1); 

            Assert.IsNotNull(photo.Title);
        }

    }
}

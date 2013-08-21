using System;
using System.IO;
using NUnit.Framework;
using FarmPhoto.Domain;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class PhotoTests// : TestBase
    {
        private readonly PhotoRepository _photoRepository;

        public PhotoTests()
        {

            _photoRepository = new PhotoRepository(new Config()); 
        }

        [Test]
        public void CreatePhotoTest()
        {
            byte[] rawData = File.ReadAllBytes(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            FileInfo info = new FileInfo(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            int fileSize = Convert.ToInt32(info.Length);

            int photoId = _photoRepository.Create(new Photo { Title = "New Photo", Description = "Blah", ImageType = "image/jpeg", PhotoData = rawData, FileSize = fileSize, UserId = 1});

            Assert.IsTrue(photoId > 0); 
        }

        [Test]
        public void GetPhotoTest()
        {

            Photo photo = _photoRepository.Get(11, true); 

            Assert.IsNotNull(photo.Title);
        }

        [Test]
        public void GetUsersPhotos()
        {

            var photos = _photoRepository.Get(1, 12, new User{UserId = 1});

            Assert.IsNotNull(photos.Count > 0);
        }

    }
}

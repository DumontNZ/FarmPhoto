using System;
using System.IO;
using NUnit.Framework;
using FarmPhoto.Domain;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class PhotoTests
    {
        [Test]
        public void CreatePhtoTest()
        {

            byte[] rawData = File.ReadAllBytes(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            FileInfo info = new FileInfo(@"D:\Users\Joel\Documents\GitHub\FarmPhoto\FarmPhoto\FarmPhoto.Website\App_Data\117700.jpg");

            int fileSize = Convert.ToInt32(info.Length);

            
            
            var photoRepository = new PhotoRepository();
            int successfull = photoRepository.Create(new Photo { Title = "New Photo", ImageType = ImageType.Jpeg, PhotoData = rawData, FileSize = fileSize });

            Assert.IsTrue(successfull == 1); 
        }

    }
}

using NUnit.Framework;
using FarmPhoto.Domain;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository.Tests
{
    [TestFixture]
    public class TagTests : TestBase
    {
        [Test]
        public void CreateTagSuccesfull()
        {
            var tagRepository = new TagRepository(new Config());

            string tags = "Cat, Fish, Cow, Sheep , Dog, Hay Bail ";

            string[] atrayOfTags = tags.Split(new[] { ',' });

            foreach (var arrayOfTag in atrayOfTags)
            {
                var tag = new Tag
                    {
                        Description =  arrayOfTag.Trim(),
                        PhotoId = 1
                    };

                Assert.IsTrue(tagRepository.Create(tag) > 0);
            }
        }
    }
}

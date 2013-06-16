using System.Linq;
using FarmPhoto.Domain;
using FarmPhoto.Repository;
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public class TagManager : ITagManager
    {
        private readonly TagRepository _tagRepository;

        public TagManager(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// Creates the tag.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="photoId">The photo id.</param>
        public void CreateTag(string tags, int photoId)
        {
            string[] arrayOfTags = tags.Split(new[] { ',' });

            foreach (var tagString in arrayOfTags)
            {
                var tag = new Tag
                {
                    Description = tagString.Trim(),
                    PhotoId = photoId
                };

                _tagRepository.Create(tag);
            }
        }

        /// <summary>
        /// Gets all the tags associated with a photo
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public IList<string> Get(int photoId)
        {
            return _tagRepository.Get(photoId).Select(tag => tag.Description).ToList();
        }
    }
}

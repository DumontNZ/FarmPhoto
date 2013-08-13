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
            foreach (var tag in ExtractTags(tags, photoId))
            {
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
       
        /// <summary>
        /// Updates all the tags corrisponding to a photo by first deleting any then recreating them 
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public void Update(string tags, int photoId)
        {
            _tagRepository.Delete(photoId);

            CreateTag(tags, photoId); 
        }

        private static IEnumerable<Tag> ExtractTags(string tags, int photoId)
        {
            var tagList = new List<Tag>();
            string[] arrayOfTags = tags.Split(new[] { ',' });

            foreach (var tagString in arrayOfTags)
            {
                var photoTag = new Tag
                {
                    Description = tagString.Trim(),
                    PhotoId = photoId
                };
                tagList.Add(photoTag);
            }
            return tagList;
        }
    }
}

using System.Collections.Generic;
using FarmPhoto.Domain;

namespace FarmPhoto.Repository
{
    public interface ITagRepository
    {
        /// <summary>
        /// Creates the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        int Create(Tag tag);

        /// <summary>
        /// Gets all the tags associated with a photo
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        IList<Tag> Get(int photoId);

        void Delete(int photoId);
    }
}
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public interface ITagManager
    {
        /// <summary>
        /// Creates the tag.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="photoId">The photo id.</param>
        void CreateTag(string tags, int photoId);

        /// <summary>
        /// Gets all the tags associated with a photo
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        IList<string> Get(int photoId);
    }
}
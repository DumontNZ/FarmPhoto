using System.Collections.Generic;
using FarmPhoto.Domain;

namespace FarmPhoto.Repository
{
    public interface IPhotoRepository
    {
        /// <summary>
        /// Creates the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        int Create(Photo photo);

        /// <summary>
        /// Gets all photos that have been approved.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        IList<Photo> Get(int numberReturned, int page);

        /// <summary>
        /// Gets the specified photo by id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        Photo Get(int photoId, bool thumbnail);

        /// <summary>
        /// Gets all the users photos.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        IList<Photo> Get(User user);
    }
}
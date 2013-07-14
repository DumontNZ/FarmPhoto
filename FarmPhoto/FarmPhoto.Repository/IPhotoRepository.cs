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
        /// Gets all photos that have been approved if its for gallery otherwise gets unapproved if adminscreen .
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="numberReturned">The number returned.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        IList<Photo> Get(int page, int numberReturned, bool approved);

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

        /// <summary>
        /// Updates the specified photo to approved.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        int Update(int id, bool approved);

        /// <summary>
        /// Soft Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        int Delete(int id);
    }
}
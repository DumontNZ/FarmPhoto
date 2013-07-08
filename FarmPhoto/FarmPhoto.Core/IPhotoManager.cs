using System.Web;
using FarmPhoto.Domain;
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public interface IPhotoManager
    {
        /// <summary>
        /// Creates the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        int CreatePhoto(Photo photo, HttpPostedFileBase file);

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        Photo Get(int id, bool thumbnail);

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<Photo> Get(User user);

        /// <summary>
        /// Gets all photos that have been approved. 20 Per page 
        /// </summary>
        /// <returns></returns>
        IList<Photo> Get(int page, int numberReturned = 20);
    }
}
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
        /// Creates the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        int CreatePhoto(Photo photo);

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        Photo Get(int id, bool thumbnail);

        /// <summary>
        /// Gets the photos added by the specified user.
        /// </summary>
        /// <param name="from">Images from.</param>
        /// <param name="to">Images to .</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<Photo> Get(int from, int to, User user);

        /// <summary>
        /// Gets photos that have been tagged with tag.
        /// </summary>
        /// <param name="from">Images from.</param>
        /// <param name="to">Images to..</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        IList<Photo> Get(int from, int to, Tag tag);

        /// <summary>
        /// Gets all photos that have been approved. 20 Per page 
        /// </summary>
        /// <returns></returns>
        IList<Photo> Get(int from, int to, bool approved = true);

        /// <summary>
        /// Updates the specified photos details.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        int Update(Photo photo);

        /// <summary>
        /// Updates the specified photo to apporved.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        int Update(int id, bool approved);

        /// <summary>
        /// Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        int Delete(int id);

        IList<Photo> Search(int from, int to, string query);
    }
}
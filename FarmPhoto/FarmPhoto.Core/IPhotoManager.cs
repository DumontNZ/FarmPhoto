using System.Collections.Generic;
using FarmPhoto.Domain;

namespace FarmPhoto.Core
{
    public interface IPhotoManager
    {
        int CreatePhoto(Photo photo);
        Photo Get(int id);
        IList<Photo> Get(User user);

        /// <summary>
        /// Gets all photos that have been approved. 
        /// </summary>
        /// <returns></returns>
        IList<Photo> Get();
    }
}
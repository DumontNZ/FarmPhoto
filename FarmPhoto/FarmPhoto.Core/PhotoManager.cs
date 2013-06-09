using System.Collections.Generic;
using FarmPhoto.Domain;
using FarmPhoto.Repository;

namespace FarmPhoto.Core
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoManager"/> class.
        /// </summary>
        /// <param name="photoRepository">The photo repository.</param>
        public PhotoManager(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        /// <summary>
        /// Creates the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public int CreatePhoto(Photo photo)
        {
           return _photoRepository.Create(photo); 
        }

        /// <summary>
        /// Gets all photos that have been approved. 
        /// </summary>
        /// <returns></returns>
        public IList<Photo> Get()
        {
            return _photoRepository.Get(); 
        }

        /// <summary>
        /// Gets the specified photo by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Photo Get(int id)
        {
            return _photoRepository.Get(id); 
        }

        /// <summary>
        /// Returns all the users photos.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IList<Photo> Get(User user)
        {
            return _photoRepository.Get(user);
        }
    }
}

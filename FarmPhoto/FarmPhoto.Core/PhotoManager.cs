using FarmPhoto.Domain;
using FarmPhoto.Repository;

namespace FarmPhoto.Core
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoManager(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public int CreatePhoto(Photo photo)
        {
           return _photoRepository.CreatePhoto(photo); 
        }
    }
}

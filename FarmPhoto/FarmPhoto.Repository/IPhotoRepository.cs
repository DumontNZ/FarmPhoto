using FarmPhoto.Domain;

namespace FarmPhoto.Repository
{
    public interface IPhotoRepository
    {
        int CreatePhoto(Photo photo);
    }
}
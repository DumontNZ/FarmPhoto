using FarmPhoto.Domain;

namespace FarmPhoto.Repository
{
    public interface IPhotoRepository
    {
        int Create(Photo photo);
    }
}
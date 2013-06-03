using FarmPhoto.Domain;

namespace FarmPhoto.Core
{
    public interface IPhotoManager
    {
        int CreatePhoto(Photo photo);
    }
}
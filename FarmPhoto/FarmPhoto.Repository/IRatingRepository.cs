using FarmPhoto.Domain;

namespace FarmPhoto.Repository
{
    public interface IRatingRepository
    {
        Rating Get(Rating rating);
        Rating Submit(Rating rating);
    }
}
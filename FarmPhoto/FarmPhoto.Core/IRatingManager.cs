using FarmPhoto.Domain;

namespace FarmPhoto.Core
{
    public interface IRatingManager
    {
        Rating Get(Rating rating); 
        Rating Submit(Rating rating);
    }
}
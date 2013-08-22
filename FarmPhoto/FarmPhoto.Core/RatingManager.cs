using FarmPhoto.Domain;
using FarmPhoto.Repository;

namespace FarmPhoto.Core
{
    public class RatingManager : IRatingManager
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingManager(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Rating Get(Rating rating)
        {
            return _ratingRepository.Get(rating);
        }

        public Rating Submit(Rating rating)
        {
            return _ratingRepository.Submit(rating); 
        }
    }
}

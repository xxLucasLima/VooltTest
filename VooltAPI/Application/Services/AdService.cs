using VooltAPI.Application.Interfaces;
using VooltAPI.Domain.Entities;

namespace VooltAPI.Application.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _repository;
        public AdService(IAdRepository repository)
        {

            _repository = repository;

        }
        public Ad Create(Ad ad)
        {
            return _repository.Create(ad);
        }

        public List<Ad> GetAll()
        {
            return _repository.GetAll();
        }

        public Ad Update(Ad ad)
        {
            return _repository.Update(ad);
        }
    }
}

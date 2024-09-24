using VooltBlazor.Models;

namespace VooltBlazor.Services
{
    public interface IAdService
    {
        Task Create(Ad ad);
        Task<IEnumerable<Ad>> GetAll();
    }
}

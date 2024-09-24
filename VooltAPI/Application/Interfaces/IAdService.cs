using Microsoft.AspNetCore.Mvc;
using VooltAPI.Domain.Entities;

namespace VooltAPI.Application.Interfaces
{
    public interface IAdService
    {
        Ad Create(Ad ad);
        List<Ad> GetAll();
    }
}

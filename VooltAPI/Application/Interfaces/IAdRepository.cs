﻿using Microsoft.AspNetCore.Mvc;
using VooltAPI.Domain.Entities;

namespace VooltAPI.Application.Interfaces
{
    public interface IAdRepository
    {
        Ad Create(Ad ad);
        Ad Update(Ad ad);
        List<Ad> GetAll();
    }
}

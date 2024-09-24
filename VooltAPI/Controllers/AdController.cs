using Microsoft.AspNetCore.Mvc;
using VooltAPI.Application.Interfaces;
using VooltAPI.Domain.Entities;

namespace VooltAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;
        public AdController(IAdService adService)
        {
            _adService = adService;
        }
        [HttpPost]
        public async Task<ActionResult<Ad>> Create(Ad ad)
        {
            if (ad == null)
            {
                return BadRequest();
            }
            var result = _adService.Create(ad);
            return result != null ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            var result = _adService.GetAll();
            return result != null ? Ok(result) : NotFound(result);
        }
    }
}
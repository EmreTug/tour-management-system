using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CappadociaTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;

        public PriceController(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }
        [HttpPost, Authorize]
        public async Task<ApiResult> Get([FromBody]Price model)
        {
            return await _priceRepository.Get(model);
        }
    }
}

using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CappadociaTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyTypeController : ControllerBase
    {
        private readonly ICurrencyTypeRepository _currencyTypeRepository;

        public CurrencyTypeController(ICurrencyTypeRepository currencyTypeRepository)
        {
            _currencyTypeRepository = currencyTypeRepository;
        }
        [HttpGet, Authorize]
        public async Task<ApiResult> Get()
        {
            return await _currencyTypeRepository.Get();
        }
    }
}

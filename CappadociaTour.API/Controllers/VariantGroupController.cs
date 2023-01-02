using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CappadociaTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantGroupController : ControllerBase
    {
        private readonly IVariantGroupRepository _variantGroupRepository;

        public VariantGroupController(IVariantGroupRepository variantGroupRepository)
        {
            _variantGroupRepository = variantGroupRepository;
        }


        [HttpPost("VariantGroupByCategoryId"), Authorize]
        public async Task<ApiResult> Get([FromBody]int id,[FromHeader] string Token)
        {
            return await _variantGroupRepository.Get(id);
        }
    }
}

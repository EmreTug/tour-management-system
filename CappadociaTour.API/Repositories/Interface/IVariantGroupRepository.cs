using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface IVariantGroupRepository
    {
        Task<ApiResult> Get(int id);
    }
}

using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface ICurrencyTypeRepository
    {
        Task<ApiResult> Get();

    }
}

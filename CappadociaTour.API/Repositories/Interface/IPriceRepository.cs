using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface IPriceRepository
    {
        Task<ApiResult> Get(Price model);
    }
}

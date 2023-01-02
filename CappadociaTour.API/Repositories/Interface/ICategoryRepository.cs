using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<ApiResult> Get();

    }
}

using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories
{
    public interface ITourRepository
    {
        Task<ApiResult> Create(Tour tour);
    }
}

using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface IReservationRepository
    {
        Task<ApiResult> Post(Reservation model, string apikey);
    }
}

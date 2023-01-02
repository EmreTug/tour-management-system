using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CappadociaTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController: ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }


        [HttpPost("Add Reservation"),Authorize]

        public async Task<ApiResult> Post([FromBody]Reservation model,[FromHeader]string apikey)
        {
            return await _reservationRepository.Post(model,apikey);
        }
    }
}

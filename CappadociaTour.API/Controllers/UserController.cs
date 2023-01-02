using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CappadociaTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {

            _userRepository = userRepository;

        }
        [HttpPost("Login")]
        public async Task<ApiResult> Login([FromBody]Login model)
        {
            
            var result=await _userRepository.Login(model);
           
            return  result; 
        }
        [HttpPost("CreateUser"), Authorize(Roles = "admin")]
        public async Task<ApiResult> CreateUser([FromBody] CreateUser model)
        {
            return await _userRepository.CreateUser(model);
        }



















       
      

    }
}

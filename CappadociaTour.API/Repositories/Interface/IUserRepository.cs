using CappadociaTour.API.Models;

namespace CappadociaTour.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<ApiResult> Login(Login model);
        Task<ApiResult> CreateUser(CreateUser model);

    }
}

using PAS_1.Libs;
using PAS_1.Models;

namespace PAS_1.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto?>> GetCurrentUserAsync();
    }
}

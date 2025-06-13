using PAS_1.Models;
using PAS_1.Data;
using PAS_1.Libs;

namespace PAS_1.Services
{
    public class UserService(TodoDbContext context, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        public async Task<ServiceResult<UserDto?>> GetCurrentUserAsync()
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            
            if (currentUserId is null)
                return ServiceResult<UserDto?>.Fail(ServiceError.Unauthorized("Token missing or invalid"));


            var user = await context.Users.FindAsync(currentUserId);
            
            if (user is null)
                return ServiceResult<UserDto?>.Fail(ServiceError.NotFound("User not found"));
            
            return ServiceResult<UserDto?>.Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
            });
        }
    }
}

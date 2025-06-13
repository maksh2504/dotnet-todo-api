using Microsoft.AspNetCore.Authorization;
using PAS_1.Services;
using Microsoft.AspNetCore.Mvc;
using PAS_1.Models;

namespace PAS_1.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("current")]
        public async Task<ActionResult<UserDto?>> GetCurrentUser()
        {
            var user = await userService.GetCurrentUserAsync();
            
            return this.ToActionResult(user);
        }
    }
}
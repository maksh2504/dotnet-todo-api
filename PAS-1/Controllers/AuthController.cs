using PAS_1.Services;
using Microsoft.AspNetCore.Mvc;
using PAS_1.Models;

namespace PAS_1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<TokenResponseDto>> Register(LoginDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");
        
            return Ok(result);
        }
    }
}
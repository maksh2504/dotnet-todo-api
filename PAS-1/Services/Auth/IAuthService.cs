using PAS_1.Models;

namespace PAS_1.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> RegisterAsync(LoginDto request);
        Task<TokenResponseDto?> LoginAsync(LoginDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}

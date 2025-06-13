using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PAS_1.Entities;
using PAS_1.Models;
using PAS_1.Data;

namespace PAS_1.Services
{
    public class AuthService(TodoDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponseDto?> RegisterAsync(LoginDto request)
        {
            if (await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.Password = hashedPassword;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return await CreateTokenResponse(user);
        }
        
        public async Task<TokenResponseDto?> LoginAsync(LoginDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var principal = ValidateJwtToken(request.RefreshToken);
            if (principal is null)
                return null;

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userIdClaim is null)
                return null;
            
            var user = await ValidateRefreshTokenAsync(int.Parse(userIdClaim), request.RefreshToken);
            if (user is null)
                return null;
        
            return await CreateTokenResponse(user);
        }
        
        private async Task<TokenResponseDto> CreateTokenResponse(User? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }
        
        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpTime <= DateTime.UtcNow)
            {
                return null;
            }
        
            return user;
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = CreateToken(user, 5 * 12 * 24 * 7);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(User user, int exp = 5)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:TokenSecret")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exp),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        
        private ClaimsPrincipal? ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["AppSettings:TokenSecret"]!);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

    }
}

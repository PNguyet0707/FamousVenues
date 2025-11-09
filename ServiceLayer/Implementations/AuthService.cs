using DataLayer.Data;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServiceLayer.Services
{
        public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
        {
            public async Task<TokenResponseDto?> LoginAsync(UserRequestDto request)
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
                if (user is null)
                {
                    return null;
                }
                if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                    == PasswordVerificationResult.Failed)
                {
                    return null;
                }

                return await CreateTokenResponse(user);
            }

            private async Task<TokenResponseDto> CreateTokenResponse(User? user)
            {
                return new TokenResponseDto
                {
                    AccessToken = GenerateToken(user),
                    RefreshToken = await GenerateRefreshTokenAsync(user)
                };
            }
            
            public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
            {
                var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
                if (user is null)
                    return null;

                return await CreateTokenResponse(user);
            }

            private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
            {
                var user = await context.Users.FindAsync(userId);
                if (user is null || user.RefreshToken != refreshToken
                    || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return null;
                }

                return user;
            }
           
            private async Task<string> GenerateRefreshTokenAsync(User user)
            {
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await context.SaveChangesAsync();
                return refreshToken;
            }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        private string GenerateToken(User user)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var tokenDescriptor = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                    audience: configuration.GetValue<string>("AppSettings:Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );            
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
        }
}

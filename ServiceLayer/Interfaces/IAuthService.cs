using DataLayer.Dtos;

namespace ServiceLayer.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(UserRequestDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}

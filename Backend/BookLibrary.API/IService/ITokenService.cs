using System.Security.Claims;
using Domain.Entities;

namespace BookLibrary.IService
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
        Task<string> GetUserIdFromTokenAsync(string token);
        int GetAccessTokenExpiryInMinutes();
        int GetRefreshTokenExpiryInDays();
    }
}
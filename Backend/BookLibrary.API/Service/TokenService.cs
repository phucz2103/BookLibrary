using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookLibrary.Data;
using BookLibrary.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.Service
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _config;

        public TokenService(UserManager<User> userManager, ApplicationDBContext context, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }
        public async Task<string> GenerateAccessToken(User user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(GetAccessTokenExpiryInMinutes()),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidAudience = _config["Jwt:Audience"],
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"])),
                ValidateLifetime = false // Chú ý: Không kiểm tra thời gian hết hạn ở đây
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
        public int GetRefreshTokenExpiryInDays()
        {
            var expiry = _config["Jwt:RefreshTokenExpiryInDays"];
            return int.TryParse(expiry, out var result) ? result : 7;
        }
        public int GetAccessTokenExpiryInMinutes()
        {
            var expiry = _config["Jwt:AccessTokenExpiryInMinutes"];
            return int.TryParse(expiry, out var result) ? result : 15;
        }
        public async Task<string> GetUserIdFromTokenAsync(string token)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(token);
                var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityTokenException("User ID not found in token");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new SecurityTokenException("User not found");
                }

                return userId;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token", ex);
            }
        }
        public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId.ToString() == userId && rt.Token == refreshToken);
            return storedToken != null && storedToken.IsActive;
        }
    }
}
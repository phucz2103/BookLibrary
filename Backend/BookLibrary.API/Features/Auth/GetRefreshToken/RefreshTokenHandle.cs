using BookLibrary.IRepositories;
using BookLibrary.IService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookLibrary.API.Features.Auth.GetRefreshToken
{
    public class RefreshTokenHandle : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RefreshTokenHandle(ITokenService tokenService, UserManager<User> userManager, IAuthRepository authRepository,IHttpContextAccessor httpContextAccessor )
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _authRepository = authRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null || request.RefreshToken is null)
            {
                throw new ArgumentNullException("Token không tồn tại");
            }
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal is null)
            {
                throw new Exception("Token không hợp lệ");
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException("Không tìm thấy userId trong token!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new UnauthorizedAccessException("Không tìm thấy user!");

            var isValidRefresh = await _tokenService.ValidateRefreshTokenAsync(userId, request.RefreshToken);
            if (!isValidRefresh) throw new UnauthorizedAccessException("Refresh token không hợp lệ hoặc đã hết hạn!");

            var newAccessToken = await _tokenService.GenerateAccessToken(user);
            var newRefreshToken = await _tokenService.GenerateRefreshToken();
            // Lưu refresh token mới vào database
           var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiryDate = DateTime.Now.AddDays(_tokenService.GetRefreshTokenExpiryInDays()),
                UserId = Guid.Parse(userId),
                Created = DateTime.Now,
                CreatedByIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown",
           };
            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}

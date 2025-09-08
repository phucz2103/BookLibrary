using BookLibrary.IRepositories;
using BookLibrary.IService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.API.Features.Auth.Login
{
    public class LoginHandle : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthRepository _authRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginHandle(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IAuthRepository authRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _authRepo = authRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)) throw new Exception("Vui lòng nhập thông tin bắt buộc!");

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new Exception("Tài khoản không tồn tại!");

            var checkLogin = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkLogin.Succeeded) throw new Exception("Sai mật khẩu!");

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken();
            var userRole = await _userManager.GetRolesAsync(user);

            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(_tokenService.GetRefreshTokenExpiryInDays()),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
            var isCreated = await _authRepo.CreateRefreshToken(newRefreshToken);
            if (!isCreated) throw new Exception("Đăng nhập không thành công, vui lòng thử lại!");

            var response = new LoginResponse
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Role = userRole.FirstOrDefault() ?? "User"
            };
            return response;
        }
    }
}
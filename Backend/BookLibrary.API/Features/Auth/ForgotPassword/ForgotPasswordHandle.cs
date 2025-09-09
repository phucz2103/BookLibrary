using BookLibrary.API.Helper;
using BookLibrary.IRepositories;
using BookLibrary.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class ForgotPasswordHandle : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<User> _userManager;
        public ForgotPasswordHandle(IAuthRepository authRepo, IMemoryCache memoryCache, UserManager<User> userManager)
        {
            _authRepo = authRepo;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var cacheKey = $"resetPasswordToken:{request.token}";
            if (!_memoryCache.TryGetValue(cacheKey, out string email))
                throw new Exception("Liên kết đã hết hạn hoặc không hợp lệ");

                if (!FormatHelper.IsValidPassword(request.newPassword))
                {
                    throw new Exception("Password không hợp lệ!");
                }
                if (request.newPassword != request.confirmNewPassword)
                {
                    throw new Exception("Xác nhận mật khẩu không khớp!");
                }
                var user = await _authRepo.GetUserByEmail(email);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, request.newPassword);

                _memoryCache.Remove($"resetPasswordToken:{request.token}");

                if (result.Succeeded)
                {
                    return "Đặt lại mật khẩu thành công";
                }
                else
                {
                    throw new Exception("Đặt lại mật khẩu thất bại");
                }
            }
    }
}

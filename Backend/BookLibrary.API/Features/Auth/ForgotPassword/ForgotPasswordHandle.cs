using BookLibrary.API.Helper;
using BookLibrary.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class ForgotPasswordHandle : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly AuthRepository _authRepo;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<User> _userManager;
        public ForgotPasswordHandle(AuthRepository authRepo, IMemoryCache memoryCache, UserManager<User> userManager)
        {
            _authRepo = authRepo;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue($"resetPasswordToken:{request.ResetPasswordToken}", out string email))
            {
                if (!FormatHelper.IsValidPassword(request.NewPassword))
                {
                    throw new Exception("Password không hợp lệ!");
                }
                if (request.NewPassword != request.ConfirmPassword)
                {
                    throw new Exception("Xác nhận mật khẩu không khớp!");
                }
                var user = await _authRepo.GetUserByEmail(email);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
                _memoryCache.Remove($"resetPasswordToken:{request.ResetPasswordToken}");
                if (result.Succeeded)
                {
                    return "Đặt lại mật khẩu thành công";
                }
                else
                {
                    throw new Exception("Đặt lại mật khẩu thất bại");
                }
            }
            else
            {
                throw new Exception("Hết thời gian thực hiện"); // thời gian thực hiện hết hạn
            }
        }
    }
}

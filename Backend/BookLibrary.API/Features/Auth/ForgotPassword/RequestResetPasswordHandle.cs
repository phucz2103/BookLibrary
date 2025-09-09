using BookLibrary.API.Helper;
using BookLibrary.API.IService;
using BookLibrary.IRepositories;
using BookLibrary.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class RequestResetPasswordHandle : IRequestHandler<RequestResetPasswordCommand, bool>
    {
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        public RequestResetPasswordHandle(IAuthRepository authRepo, IEmailService emailService, IMemoryCache memoryCache)
        {
            _authRepo = authRepo;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }
        public async Task<bool> Handle(RequestResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if(FormatHelper.IsValidEmail(request.Email) == false)
            {
                throw new Exception("Email không hợp lệ!");
            }

            var userExists = await _authRepo.GetUserByEmail(request.Email);
            if (userExists == null)
            {
                return true;
            }
            var resetPasswordToken = Guid.NewGuid().ToString();
            _memoryCache.Set($"resetPasswordToken:{resetPasswordToken}", request.Email, TimeSpan.FromMinutes(15));

            var resetLink = $"http://localhost:5173/reset-password?token={resetPasswordToken}";
            var subject = "Yêu cầu đặt lại mật khẩu - Thư viện XP";
            var message = $@"
            <p>Xin chào,</p>
            <p>Bạn đã yêu cầu đặt lại mật khẩu.</p>
            <p>Nhấn vào liên kết sau để đặt lại mật khẩu:</p>
            <p><a href='{resetLink}' style='color:blue;font-size:16px;'>{resetLink}</a></p>
            <p>Liên kết này sẽ hết hạn sau <strong>15 phút</strong>.</p>
            <p>Trân trọng,<br><b>Thư viện XP</b></p>";

            try
            {
                await _emailService.SendEmailAsync(request.Email, subject, message);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gửi email thất bại: " + ex.Message);
            }
        }
    }
}

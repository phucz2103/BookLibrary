using BookLibrary.API.Helper;
using BookLibrary.API.IService;
using BookLibrary.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibrary.API.Features.Auth.OTP
{
    public class RequestOTPHandle : IRequestHandler<RequestOTPCommand, bool>
    {
        private readonly AuthRepository _authRepo;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        public RequestOTPHandle(AuthRepository authRepo, IEmailService emailService, IMemoryCache memoryCache)
        {
            _authRepo = authRepo;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }
        public async Task<bool> Handle(RequestOTPCommand request, CancellationToken cancellationToken)
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

            var otpCode = GenerateStringHelper.GenerateOTP();

            var subject = "Xác thực OTP từ thư viện XP";

            var messasge = $@"
                                  <p>Xin chào,</p>
                                  <p>Bạn đã yêu cầu xác thực bằng mã OTP.</p>
                                  <p><strong>Mã OTP của bạn là:</strong> <b style='font-size: 18px; color: blue;'>{otpCode}</b></p>
                                  <p>Mã này sẽ hết hạn sau <strong>5 phút</strong>. Vui lòng không chia sẻ mã với bất kỳ ai.</p>
                                  <p>Trân trọng,<br><b>Thư viện XP</b></p>";
            try
            {
                await _emailService.SendEmailAsync(request.Email, subject, messasge);
            }
            catch (Exception ex)
            {
                throw new Exception("Gửi email thất bại: " + ex.Message);
            }

            var otp = new OTPdto
            {
                Email = request.Email,
                Code = otpCode,
                ExpiryTime = DateTime.Now.AddMinutes(5)
            };
            _memoryCache.Set($"OTP_{request.Email}", otp, otp.ExpiryTime - DateTime.Now);
            return true;
        }
    }
}

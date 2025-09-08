using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BookLibrary.API.Features.Auth.OTP
{
    public class VerifyOTPHandle : IRequestHandler<VerifyOTPCommand, string>
    {
        private readonly IMemoryCache _memoryCache;
        public VerifyOTPHandle(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<string> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.OTP)) throw new Exception("Mã OTP không được để trống!");
            if (_memoryCache.TryGetValue($"OTP_{request.Email}", out OTPdto cachedOtp))
            {
                if (cachedOtp.Code == request.OTP && cachedOtp.ExpiryTime > DateTime.Now)
                {
                    _memoryCache.Remove($"otp:{request.Email}");
                    var resetPasswordToken = Guid.NewGuid().ToString();
                    _memoryCache.Set($"resetPasswordToken:{resetPasswordToken}", request.Email, TimeSpan.FromMinutes(15)); // Token hợp lệ trong 15 phút
                    return resetPasswordToken;
                }
                else
                {
                    throw new Exception("Mã OTP không hợp lệ"); // Mã OTP không đúng
                }
            }
            else
            {
                throw new Exception("OTP đã hết hiệu lực"); // thời gian thực hiện hết hạn
            }

        }
    }
}

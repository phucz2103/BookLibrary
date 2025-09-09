using MediatR;

namespace BookLibrary.API.Features.Auth.OTP
{
    public class VerifyOTPCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? OTP { get; set; }
    }
}

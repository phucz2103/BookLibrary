using MediatR;

namespace BookLibrary.API.Features.Auth.OTP
{
    public class RequestOTPCommand : IRequest<bool>
    {
        public string? Email { get; set; }

    }
}

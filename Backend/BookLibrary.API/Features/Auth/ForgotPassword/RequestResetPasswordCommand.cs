using MediatR;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class RequestResetPasswordCommand : IRequest<bool>
    {
        public string? Email { get; set; }
    }
}

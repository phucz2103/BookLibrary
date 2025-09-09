using MediatR;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}

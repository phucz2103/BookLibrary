using MediatR;

namespace BookLibrary.API.Features.Auth.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
        public string token { get; set; }
    }
}

using MediatR;

namespace BookLibrary.API.Features.Auth.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
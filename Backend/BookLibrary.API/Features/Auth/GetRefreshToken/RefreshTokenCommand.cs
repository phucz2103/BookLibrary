using MediatR;

namespace BookLibrary.API.Features.Auth.GetRefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

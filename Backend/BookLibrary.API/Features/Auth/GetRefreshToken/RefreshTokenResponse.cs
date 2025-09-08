namespace BookLibrary.API.Features.Auth.GetRefreshToken
{
    public class RefreshTokenResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

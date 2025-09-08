namespace BookLibrary.API.Features.Auth.Login
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
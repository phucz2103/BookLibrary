namespace BookLibrary.API.Features.Auth.OTP
{
    public class OTPdto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}

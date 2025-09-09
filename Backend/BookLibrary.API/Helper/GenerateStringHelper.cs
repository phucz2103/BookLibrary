namespace BookLibrary.API.Helper
{
    public static class GenerateStringHelper
    {
        public static string GenerateOTP()
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            return otp;
        }
    }
}

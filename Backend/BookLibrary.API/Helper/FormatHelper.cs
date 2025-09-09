using System.Text.RegularExpressions;

namespace BookLibrary.API.Helper
{
    public static class FormatHelper
    {
        public static bool FormatPhoneNumber(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            var digits = new string(input.Where(char.IsDigit).ToArray());

            return digits.Length == 10 && digits.StartsWith("0");
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}

namespace BlogApplication.Models.Constants.RegularExpressions
{
    public static class RegexPatterns
    {
        public const string AlphaNumeric = "^[a-zA-Z0-9_]*$";
        public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    }
}
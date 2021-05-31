namespace BlogApplication.Models.Constants.Messages
{
    public static class ValidationErrorMessages
    {
        public const string EmailIsNotValid = "The Email field is not a valid e-mail address.";
        public const string MustBeAlphaNumeric0 = "The {0} must be alpha numeric.";
        public const string MustBeAtLeast01 = "The {0} must be at least {1} characters long.";
        public const string MustBeAtLeast02 = "The {0} must be at least {2} characters long.";
        public const string PasswordDoesNotMatch = "The password and confirmation password do not match.";
        public const string ValueCannotExceed01 = "The {0} value cannot exceed {1} characters.";
    }
}
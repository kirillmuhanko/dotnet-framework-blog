using System.Collections.Generic;
using BlogApplication.Models.Enums;

namespace BlogApplication.Models.Constants.Messages
{
    public static class AuthenticationMessages
    {
        public const string InvalidLoginAttempt = "Invalid login attempt.";

        public static readonly IDictionary<AuthenticationStatus, string> MessagesByType =
            new Dictionary<AuthenticationStatus, string>
            {
                {AuthenticationStatus.ChangePasswordSuccess, "Your password has been changed."},
                {AuthenticationStatus.SetPasswordSuccess, "Your password has been set."},
                {
                    AuthenticationStatus.SetTwoFactorSuccess,
                    "Your two-factor authentication provider has been set."
                },
                {AuthenticationStatus.Error, "An error has occurred."},
                {AuthenticationStatus.AddPhoneSuccess, "Your phone number was added."},
                {AuthenticationStatus.RemovePhoneSuccess, "Your phone number was removed."}
            };
    }
}
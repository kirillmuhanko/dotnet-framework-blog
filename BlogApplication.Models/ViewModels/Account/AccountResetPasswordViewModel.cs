using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Constants.RegularExpressions;
using BlogApplication.Models.ViewModels.Base;

namespace BlogApplication.Models.ViewModels.Account
{
    public class AccountResetPasswordViewModel : ViewModelBase
    {
        public AccountResetPasswordViewModel()
        {
            ConfirmPassword = new PasswordFormGroupComponent();
            Password = new PasswordFormGroupComponent();
            Email = new TextBoxFormGroupComponent();
        }

        [CompareFormGroup("Password", ErrorMessage = ValidationErrorMessages.PasswordDoesNotMatch)]
        [Display(Name = "Confirm password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent ConfirmPassword { get; set; }

        [Display(Name = "Password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent Password { get; set; }

        public string Code { get; set; }

        [Display(Name = "Email")]
        [RegexFormGroup(RegexPatterns.Email, ErrorMessage = ValidationErrorMessages.EmailIsNotValid)]
        [RequiredFormGroup]
        public TextBoxFormGroupComponent Email { get; set; }

        public override void ResetValidation()
        {
            ConfirmPassword.ResetValidation();
            Password.ResetValidation();
            Email.ResetValidation();
        }
    }
}
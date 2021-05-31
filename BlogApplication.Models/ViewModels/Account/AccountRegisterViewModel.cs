using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Constants.RegularExpressions;
using BlogApplication.Models.ViewModels.Base;

namespace BlogApplication.Models.ViewModels.Account
{
    public class AccountRegisterViewModel : ViewModelBase
    {
        public AccountRegisterViewModel()
        {
            ConfirmPassword = new PasswordFormGroupComponent();
            Password = new PasswordFormGroupComponent();
            Email = new TextBoxFormGroupComponent();
            UserName = new UserNameFormGroupComponent();
        }

        [CompareFormGroup("Password", ErrorMessage = ValidationErrorMessages.PasswordDoesNotMatch)]
        [Display(Name = "Confirm password")]
        [RequiredFormGroup]
        public PasswordFormGroupComponent ConfirmPassword { get; set; }

        [Display(Name = "Password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent Password { get; set; }

        [Display(Name = "Email")]
        [RegexFormGroup(RegexPatterns.Email, ErrorMessage = ValidationErrorMessages.EmailIsNotValid)]
        [RequiredFormGroup]
        public TextBoxFormGroupComponent Email { get; set; }

        [Display(Name = "User Name")]
        [MaxLengthFormGroup(20, ErrorMessage = ValidationErrorMessages.ValueCannotExceed01)]
        [MinLengthFormGroup(6, ErrorMessage = ValidationErrorMessages.MustBeAtLeast01)]
        [RegexFormGroup(RegexPatterns.AlphaNumeric, ErrorMessage = ValidationErrorMessages.MustBeAlphaNumeric0)]
        [RequiredFormGroup]
        public UserNameFormGroupComponent UserName { get; set; }

        public override void ResetValidation()
        {
            ConfirmPassword.ResetValidation();
            Password.ResetValidation();
            Email.ResetValidation();
            UserName.ResetValidation();
        }
    }
}
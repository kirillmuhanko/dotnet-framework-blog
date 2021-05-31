using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.ViewModels.Base;

namespace BlogApplication.Models.ViewModels.Manage
{
    public class ManageChangePasswordViewModel : ViewModelBase
    {
        public ManageChangePasswordViewModel()
        {
            ConfirmPassword = new PasswordFormGroupComponent();
            NewPassword = new PasswordFormGroupComponent();
            OldPassword = new PasswordFormGroupComponent();
        }

        [CompareFormGroup("NewPassword", ErrorMessage = ValidationErrorMessages.PasswordDoesNotMatch)]
        [Display(Name = "Confirm new password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent ConfirmPassword { get; set; }

        [Display(Name = "New password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent NewPassword { get; set; }

        [Display(Name = "Current password")]
        [RequiredFormGroup]
        [StringLengthFormGroup(100, ErrorMessage = ValidationErrorMessages.MustBeAtLeast02, MinimumLength = 6)]
        public PasswordFormGroupComponent OldPassword { get; set; }

        public override void ResetValidation()
        {
            ConfirmPassword.ResetValidation();
            NewPassword.ResetValidation();
            OldPassword.ResetValidation();
        }
    }
}
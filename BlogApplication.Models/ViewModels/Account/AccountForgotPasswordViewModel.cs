using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Constants.RegularExpressions;

namespace BlogApplication.Models.ViewModels.Account
{
    public class AccountForgotPasswordViewModel
    {
        public AccountForgotPasswordViewModel()
        {
            Email = new TextBoxFormGroupComponent();
        }

        [Display(Name = "Email")]
        [RegexFormGroup(RegexPatterns.Email, ErrorMessage = ValidationErrorMessages.EmailIsNotValid)]
        [RequiredFormGroup]
        public TextBoxFormGroupComponent Email { get; set; }
    }
}
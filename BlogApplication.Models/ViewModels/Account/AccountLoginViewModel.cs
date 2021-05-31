using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Models.ViewModels.Base;

namespace BlogApplication.Models.ViewModels.Account
{
    public class AccountLoginViewModel : ViewModelBase
    {
        public AccountLoginViewModel()
        {
            RememberMe = new CheckBoxFormGroupComponent();
            Password = new PasswordFormGroupComponent();
            UserName = new UserNameFormGroupComponent();
        }

        [Display(Name = "Remember me")] public CheckBoxFormGroupComponent RememberMe { get; set; }

        [Display(Name = "Password")]
        [RequiredFormGroup]
        public PasswordFormGroupComponent Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "User Name")]
        [RequiredFormGroup]
        public UserNameFormGroupComponent UserName { get; set; }

        public override void ResetValidation()
        {
            RememberMe.ResetValidation();
            Password.ResetValidation();
            UserName.ResetValidation();
        }
    }
}
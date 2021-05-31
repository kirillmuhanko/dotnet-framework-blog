using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Enums;
using BlogApplication.Models.ViewModels.Manage;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Web.Controllers
{
    [Authorize]
    public class ManageController : WebControllerBase
    {
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IUserAuthenticationManager _userAuthenticationManager;
        private readonly IUserManager _userManager;
        private readonly IUserSignInManager _userSignInManager;

        public ManageController(
            IHttpContextProvider httpContextProvider,
            IUserAuthenticationManager userAuthenticationManager,
            IUserManager userManager,
            IUserSignInManager userSignInManager)
        {
            _httpContextProvider = httpContextProvider;
            _userAuthenticationManager = userAuthenticationManager;
            _userManager = userManager;
            _userSignInManager = userSignInManager;
        }

        public ActionResult ChangePassword()
        {
            var model = new ManageChangePasswordViewModel();
            var viewResult = View(model);
            return viewResult;
        }

        public async Task<ActionResult> Index(AuthenticationStatus? message)
        {
            var model = new ManageViewModel
            {
                HasPassword = await _userManager.UserHasPasswordAsync(_httpContextProvider.UserId),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(_httpContextProvider.UserId),
                BrowserRemembered =
                    await _userAuthenticationManager.TwoFactorBrowserRememberedAsync(_httpContextProvider.UserId),
                StatusMessage = AuthenticationMessages.MessagesByType.GetStringOrEmpty(message)
            };

            var viewResult = View(model);
            return viewResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ManageChangePasswordViewModel model)
        {
            var viewResult = View(model);

            if (ModelHasErrors) return viewResult;

            var status = await _userManager.ChangePasswordAsync(
                _httpContextProvider.UserId,
                model.OldPassword.Text,
                model.NewPassword.Text);

            if (status.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(_httpContextProvider.UserId);
                if (user != null) await _userSignInManager.SignInAsync(user, false, false);

                var redirectToRouteResult = RedirectToAction(
                    ActionNames.Index,
                    new {Message = AuthenticationStatus.ChangePasswordSuccess});

                return redirectToRouteResult;
            }

            AddModelErrors(model, status.Errors);
            return viewResult;
        }
    }
}
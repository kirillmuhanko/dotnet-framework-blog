using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Builders.Strings;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;
using BlogApplication.Models.ViewModels.Account;
using Microsoft.AspNet.Identity;

namespace BlogApplication.Web.Controllers
{
    [Authorize]
    public class AccountController : WebControllerBase
    {
        private readonly IModelMapper _modelMapper;
        private readonly IUserAuthenticationManager _userAuthenticationManager;
        private readonly IUserManager _userManager;
        private readonly IUserSignInManager _userSignInManager;

        public AccountController(
            IModelMapper modelMapper,
            IUserAuthenticationManager userAuthenticationManager,
            IUserManager userManager,
            IUserSignInManager userSignInManager)
        {
            _modelMapper = modelMapper;
            _userAuthenticationManager = userAuthenticationManager;
            _userManager = userManager;
            _userSignInManager = userSignInManager;
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new AccountForgotPasswordViewModel();
            var viewResult = View(model);
            return viewResult;
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            var viewResult = View();
            return viewResult;
        }

        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };

            var viewResult = View(model);
            return viewResult;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new AccountRegisterViewModel();
            var viewResult = View(model);
            return viewResult;
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            var model = new AccountResetPasswordViewModel();
            var viewResult = code == null ? View(ViewNames.Error) : View(model);
            return viewResult;
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            var viewResult = View();
            return viewResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _userAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var redirectToRouteResult = RedirectToAction(ActionNames.Index, ControllerNames.Home);
            return redirectToRouteResult;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(AccountForgotPasswordViewModel model)
        {
            if (ModelHasErrors)
            {
                var viewResult = View(model);
                return viewResult;
            }

            var user = await _userManager.FindByEmailAsync(model.Email.Text);
            if (user == null) return View(ActionNames.ForgotPasswordConfirmation);
            var password = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.Action(
                ActionNames.ResetPassword,
                ControllerNames.Account,
                new {userId = user.Id, code = password},
                UrlScheme);

            await _userManager.SendEmailAsync(
                user.Id,
                SubjectMessages.ResetPassword,
                EmailMessageBuilder.BuildResetYourPasswordText(callbackUrl));

            var redirectToRouteResult = RedirectToAction(
                ActionNames.ForgotPasswordConfirmation,
                ControllerNames.Account);

            return redirectToRouteResult;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(AccountLoginViewModel model)
        {
            var viewResult = View(model);
            if (ModelHasErrors) return viewResult;

            var result = await _userSignInManager.PasswordSignInAsync(
                model.UserName.Text,
                model.Password.Text,
                model.RememberMe.IsChecked);

            if (result == UserSignInStatus.Success)
            {
                var actionResult = RedirectToLocal(model.ReturnUrl);
                return actionResult;
            }

            AddModelError(model, AuthenticationMessages.InvalidLoginAttempt);
            return viewResult;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegisterViewModel model)
        {
            var viewResult = View(model);
            if (ModelHasErrors) return viewResult;

            var user = _modelMapper.Map<AccountRegisterViewModel, UserEntity>(model);
            var status = await _userManager.CreateAsync(user, model.Password.Text);

            if (status.Succeeded)
            {
                await _userSignInManager.SignInAsync(user, false, false);
                var redirectToRouteResult = RedirectToAction(ActionNames.Index, ControllerNames.Home);
                return redirectToRouteResult;
            }

            AddModelErrors(model, status.Errors);
            return viewResult;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(AccountResetPasswordViewModel model)
        {
            var viewResult = View(model);
            if (ModelHasErrors) return viewResult;

            var user = await _userManager.FindByEmailAsync(model.Email.Text);
            var status = await _userManager.ResetPasswordAsync(user?.Id, model.Code, model.Password.Text);

            if (status.Succeeded)
            {
                var redirectToRouteResult = RedirectToAction(
                    ActionNames.ResetPasswordConfirmation,
                    ControllerNames.Account);

                return redirectToRouteResult;
            }

            AddModelErrors(model, status.Errors);
            return viewResult;
        }
    }
}
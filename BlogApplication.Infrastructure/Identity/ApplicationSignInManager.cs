using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace BlogApplication.Infrastructure.Identity
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        private ApplicationSignInManager(
            ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(
            IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationSignInManager(
                context.GetUserManager<ApplicationUserManager>(),
                context.Authentication);

            return manager;
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var task = user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
            return task;
        }
    }
}
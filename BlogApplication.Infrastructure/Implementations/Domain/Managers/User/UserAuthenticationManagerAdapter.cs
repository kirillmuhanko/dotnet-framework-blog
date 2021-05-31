using System.Threading.Tasks;
using System.Web;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Enums;
using Microsoft.Owin.Security;

namespace BlogApplication.Infrastructure.Implementations.Domain.Managers.User
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class UserAuthenticationManagerAdapter : IUserAuthenticationManager
    {
        public async Task<bool> TwoFactorBrowserRememberedAsync(string id)
        {
            var isOk = await Manager.TwoFactorBrowserRememberedAsync(id);
            return isOk;
        }

        public void SignOut(string[] authenticationTypes)
        {
            Manager.SignOut(authenticationTypes);
        }

        private static IAuthenticationManager Manager => HttpContext.Current.GetOwinContext()?.Authentication;
    }
}
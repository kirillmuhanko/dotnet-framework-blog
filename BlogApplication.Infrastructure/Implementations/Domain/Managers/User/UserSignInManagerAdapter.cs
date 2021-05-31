using System.Threading.Tasks;
using System.Web;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Infrastructure.Identity;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;
using Microsoft.AspNet.Identity.Owin;

namespace BlogApplication.Infrastructure.Implementations.Domain.Managers.User
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class UserSignInManagerAdapter : IUserSignInManager
    {
        private readonly IModelMapper _modelMapper;

        public UserSignInManagerAdapter(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public async Task SignInAsync(UserEntity entity, bool isPersistent, bool rememberBrowser)
        {
            var model = _modelMapper.Map<UserEntity, ApplicationUser>(entity);
            await Manager.SignInAsync(model, isPersistent, rememberBrowser);
        }

        public async Task<UserSignInStatus> PasswordSignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool shouldLockout = false)
        {
            var result = await Manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            var model = (UserSignInStatus) (int) result;
            return model;
        }

        private static ApplicationSignInManager Manager =>
            HttpContext.Current.GetOwinContext()?.GetUserManager<ApplicationSignInManager>();
    }
}
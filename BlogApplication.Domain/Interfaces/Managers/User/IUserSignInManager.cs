using System.Threading.Tasks;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Interfaces.Managers.User
{
    public interface IUserSignInManager
    {
        Task SignInAsync(UserEntity entity, bool isPersistent, bool rememberBrowser);

        Task<UserSignInStatus> PasswordSignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool shouldLockout = false);
    }
}
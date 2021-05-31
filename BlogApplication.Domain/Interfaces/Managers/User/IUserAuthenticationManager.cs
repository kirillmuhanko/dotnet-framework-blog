using System.Threading.Tasks;

namespace BlogApplication.Domain.Interfaces.Managers.User
{
    public interface IUserAuthenticationManager
    {
        Task<bool> TwoFactorBrowserRememberedAsync(string id);

        void SignOut(params string[] authenticationTypes);
    }
}
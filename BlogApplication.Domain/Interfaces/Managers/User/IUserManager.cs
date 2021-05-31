using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Interfaces.Managers.User
{
    public interface IUserManager
    {
        Task SendEmailAsync(string id, string subject, string body);

        Task<bool> IsEmailConfirmedAsync(string id);

        Task<bool> IsInRoleAsync(string id, string role);

        Task<bool> UserHasPasswordAsync(string id);

        Task<IEnumerable<UserEntity>> GetUsersAsync(PaginationDomainModel pagination);

        Task<IEnumerable<UserRoleDomainModel>> GetRolesAsync(string userId);

        Task<int> AddToRoleAsync(string id, string roleName);

        Task<int> UpdateRoles(string userId, IEnumerable<UserRoleDomainModel> roles);

        Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize);

        Task<ResultStatusDomainModel> ChangePasswordAsync(string id, string currentPassword, string newPassword);

        Task<ResultStatusDomainModel> CreateAsync(UserEntity entity, string password);

        Task<ResultStatusDomainModel> ResetPasswordAsync(string id, string token, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(string id);

        Task<string> GetPhoneNumberAsync(string id);

        Task<UserEntity> FindByEmailAsync(string email);

        Task<UserEntity> FindByIdAsync(string id);

        Task<UserEntity> FindByNameAsync(string userName);
    }
}
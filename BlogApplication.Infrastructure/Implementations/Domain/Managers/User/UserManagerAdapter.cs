using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Predicates.DomainModels;
using BlogApplication.Domain.Interfaces.Services.User;
using BlogApplication.Infrastructure.Database;
using BlogApplication.Infrastructure.Identity;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;
using BlogApplication.Shared.Interfaces.Facades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BlogApplication.Infrastructure.Implementations.Domain.Managers.User
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class UserManagerAdapter : IUserManager
    {
        private readonly IGuidFacade _guidFacade;
        private readonly IModelMapper _modelMapper;
        private readonly IUserRoleDomainModelPredicates _userRoleDomainModelPredicates;
        private readonly IUserService _userService;

        public UserManagerAdapter(
            IGuidFacade guidFacade,
            IModelMapper modelMapper,
            IUserRoleDomainModelPredicates userRoleDomainModelPredicates,
            IUserService userService)
        {
            _guidFacade = guidFacade;
            _modelMapper = modelMapper;
            _userRoleDomainModelPredicates = userRoleDomainModelPredicates;
            _userService = userService;
        }

        public async Task SendEmailAsync(string id, string subject, string body)
        {
            await Manager.SendEmailAsync(id, subject, body);
        }

        public async Task<bool> IsEmailConfirmedAsync(string id)
        {
            var result = await Manager.IsEmailConfirmedAsync(id);
            return result;
        }

        public async Task<bool> IsInRoleAsync(string id, string role)
        {
            if (string.IsNullOrEmpty(id)) return false;
            var result = await Manager.IsInRoleAsync(id, role);
            return result;
        }

        public async Task<bool> UserHasPasswordAsync(string id)
        {
            var user = await FindByIdAsync(id);
            var result = user?.PasswordHash != null;
            return result;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync(PaginationDomainModel pagination)
        {
            var enumerable = await _userService.GetUsersAsync(pagination);
            return enumerable;
        }

        public async Task<IEnumerable<UserRoleDomainModel>> GetRolesAsync(string userId)
        {
            var enumerable = await _userService.GetRolesAsync(userId);
            return enumerable;
        }

        public async Task<int> AddToRoleAsync(string id, string roleName)
        {
            await Manager.AddToRoleAsync(id, roleName);
            var result = await Context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateRoles(string userId, IEnumerable<UserRoleDomainModel> roles)
        {
            var list = roles.ToList();

            var rolesToAdd = list
                .Where(_userRoleDomainModelPredicates.ByIsChecked(true))
                .Select(_userRoleDomainModelPredicates.ByName()).ToArray();

            var rolesToRemove = list
                .Where(_userRoleDomainModelPredicates.ByIsChecked(false))
                .Select(_userRoleDomainModelPredicates.ByName()).ToArray();

            foreach (var role in rolesToAdd)
                await Manager.AddToRoleAsync(userId, role);

            foreach (var role in rolesToRemove)
                await Manager.RemoveFromRoleAsync(userId, role);

            var result = await Context.SaveChangesAsync();
            return result;
        }

        public async Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize)
        {
            var model = await _userService.GetPaginationAsync(pageNumber, pageSize);
            return model;
        }

        public async Task<ResultStatusDomainModel> ChangePasswordAsync(
            string id,
            string currentPassword,
            string newPassword)
        {
            var result = await Manager.ChangePasswordAsync(id, currentPassword, newPassword);
            await Context.SaveChangesAsync();
            var model = _modelMapper.Map<IdentityResult, ResultStatusDomainModel>(result);
            return model;
        }

        public async Task<ResultStatusDomainModel> CreateAsync(UserEntity entity, string password)
        {
            var guid = _guidFacade.NewGuid();
            entity.Id = _guidFacade.ToString(guid);
            var user = _modelMapper.Map<UserEntity, ApplicationUser>(entity);
            var result = await Manager.CreateAsync(user, password);
            await Context.SaveChangesAsync();
            var model = _modelMapper.Map<IdentityResult, ResultStatusDomainModel>(result);
            return model;
        }

        public async Task<ResultStatusDomainModel> ResetPasswordAsync(
            string id,
            string token,
            string newPassword)
        {
            var result = await Manager.ResetPasswordAsync(id, token, newPassword);
            await Context.SaveChangesAsync();
            var model = _modelMapper.Map<IdentityResult, ResultStatusDomainModel>(result);
            return model;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string id)
        {
            var str = await Manager.GeneratePasswordResetTokenAsync(id);
            return str;
        }

        public async Task<string> GetPhoneNumberAsync(string id)
        {
            var str = await Manager.GetPhoneNumberAsync(id);
            return str;
        }

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            var model = await Manager.FindByEmailAsync(email);
            var user = _modelMapper.Map<ApplicationUser, UserEntity>(model);
            return user;
        }

        public async Task<UserEntity> FindByIdAsync(string id)
        {
            var model = await Manager.FindByIdAsync(id);
            var user = _modelMapper.Map<ApplicationUser, UserEntity>(model);
            return user;
        }

        public async Task<UserEntity> FindByNameAsync(string userName)
        {
            var model = await Manager.FindByNameAsync(userName);
            var user = _modelMapper.Map<ApplicationUser, UserEntity>(model);
            return user;
        }

        private static ApplicationDbContext Context =>
            HttpContext.Current.GetOwinContext()?.Get<ApplicationDbContext>();

        private static ApplicationUserManager Manager =>
            HttpContext.Current.GetOwinContext()?.GetUserManager<ApplicationUserManager>();
    }
}
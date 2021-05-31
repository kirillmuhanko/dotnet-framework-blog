using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.User;
using BlogApplication.Domain.Interfaces.Predicates.Entities;
using BlogApplication.Domain.Interfaces.Services.User;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Domain.Services.User
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class UserService : IUserService
    {
        private readonly IRepository<RoleEntity> _roleRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRoleEntityPredicates _roleEntityPredicates;
        private readonly IUserExpressions _userExpressions;

        public UserService(
            IRepository<RoleEntity> roleRepository,
            IRepository<UserEntity> userRepository,
            IRoleEntityPredicates roleEntityPredicates,
            IUserExpressions userExpressions)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _roleEntityPredicates = roleEntityPredicates;
            _userExpressions = userExpressions;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync(PaginationDomainModel pagination)
        {
            var query = _userRepository.QueryByExpression(_userExpressions.ById());
            query = query.OrderBy(_userExpressions.ByUserName());
            query = query.Page(pagination);
            var list = await _userRepository.ToListAsync(query);
            return list;
        }

        public async Task<IEnumerable<UserRoleDomainModel>> GetRolesAsync(string userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(_userExpressions.ById(userId));
            if (user == null) return new List<UserRoleDomainModel>();
            var roleEntities = await _roleRepository.ToListAsync();
            await _userRepository.LoadCollectionAsync(user, _userExpressions.ByRoles());
            var roleModels = roleEntities.Select(_roleEntityPredicates.MapToUserRoleDomainModel(user.Roles));
            return roleModels;
        }

        public async Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize)
        {
            var expression = _userExpressions.ById();
            var count = await _userRepository.CountAsync(expression);

            var model = new PaginationDomainModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = count
            };

            model.Clamp();
            return model;
        }
    }
}
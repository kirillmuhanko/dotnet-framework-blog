using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetUsersAsync(PaginationDomainModel pagination);

        Task<IEnumerable<UserRoleDomainModel>> GetRolesAsync(string userId);

        Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.Entities.Base;

namespace BlogApplication.Domain.Interfaces.Services.Common
{
    public interface IEntityService<TEntity> where TEntity : EntityBase
    {
        Task UpdateAsync(TEntity entity, string userId = null);

        Task<IEnumerable<TEntity>> GetByCreationTime(int count, bool orderByDescending = false);

        Task<IEnumerable<TEntity>> GetByCreationTime(
            PaginationDomainModel pagination,
            string userId = null,
            bool orderByDescending = false);

        Task<PaginationDomainModel> GetPaginationAsync(
            int pageNumber,
            int pageSize,
            string userId = null);

        Task<TEntity> GetByIdAsync(long id, string userId = null);

        Task<TEntity> GetByUserIdAsync(string userId);

        Task<TEntity> GetFirstOrDefaultAsync();

        Task<TEntity> RemoveAsync(long id, string userId = null);

        TEntity Add(TEntity entity);
    }
}
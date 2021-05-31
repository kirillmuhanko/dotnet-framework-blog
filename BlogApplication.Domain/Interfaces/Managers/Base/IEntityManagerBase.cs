using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.Entities.Base;

namespace BlogApplication.Domain.Interfaces.Managers.Base
{
    public interface IEntityManagerBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetListOrderedByDateTime(int count, bool orderByDescending = false);

        Task<IEnumerable<TEntity>> GetListOrderedByDateTime(
            PaginationDomainModel pagination,
            string userId = null,
            bool orderByDescending = false);

        Task<int> DeleteAsync(long id, string userId = null);

        Task<int> UpdateAsync(TEntity entity, string userId = null);

        Task<PaginationDomainModel> GetPaginationAsync(
            int pageNumber,
            int pageSize,
            string userId = null);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(long id, string userId = null);

        Task<TEntity> GetByUserIdAsync(string userId);

        Task<TEntity> GetFirstOrDefaultAsync();
    }
}
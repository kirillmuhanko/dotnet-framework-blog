using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.Entities.Base;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Managers.Base
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public abstract class EntityManagerBase<TEntity> : IEntityManagerBase<TEntity> where TEntity : EntityBase
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IEntityService<TEntity> _entityService;

        protected EntityManagerBase(
            IDatabaseContext databaseContext,
            IEntityService<TEntity> entityService)
        {
            _databaseContext = databaseContext;
            _entityService = entityService;
        }

        public async Task<IEnumerable<TEntity>> GetListOrderedByDateTime(int count, bool orderByDescending = false)
        {
            var enumerable = await _entityService.GetByCreationTime(count, orderByDescending);
            return enumerable;
        }

        public async Task<IEnumerable<TEntity>> GetListOrderedByDateTime(
            PaginationDomainModel pagination,
            string userId = null,
            bool orderByDescending = false)
        {
            var enumerable = await _entityService.GetByCreationTime(pagination, userId, orderByDescending);
            return enumerable;
        }

        public async Task<int> DeleteAsync(long id, string userId = null)
        {
            await _entityService.RemoveAsync(id, userId);
            var result = await _databaseContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(TEntity entity, string userId = null)
        {
            await _entityService.UpdateAsync(entity, userId);
            var result = await _databaseContext.SaveChangesAsync();
            return result;
        }

        public async Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize, string userId = null)
        {
            var model = await _entityService.GetPaginationAsync(pageNumber, pageSize, userId);
            return model;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = _entityService.Add(entity);
            await _databaseContext.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<TEntity> GetByIdAsync(long id, string userId = null)
        {
            var entity = await _entityService.GetByIdAsync(id, userId);
            return entity;
        }

        public async Task<TEntity> GetByUserIdAsync(string userId)
        {
            var entity = await _entityService.GetByUserIdAsync(userId);
            return entity;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync()
        {
            var entity = await _entityService.GetFirstOrDefaultAsync();
            return entity;
        }
    }
}
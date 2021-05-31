using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Builders.Queries;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.Entities.Base;
using BlogApplication.Models.Enums;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Domain.Services.Common
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public sealed class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        private readonly IEntityExpressions<TEntity> _expressions;
        private readonly IEntityQueryBuilder<TEntity> _queryBuilder;
        private readonly IModelMapper _modelMapper;
        private readonly IRepository<TEntity> _repository;

        public EntityService(
            IEntityExpressions<TEntity> expressions,
            IEntityQueryBuilder<TEntity> queryBuilder,
            IModelMapper modelMapper,
            IRepository<TEntity> repository)
        {
            _expressions = expressions;
            _queryBuilder = queryBuilder;
            _modelMapper = modelMapper;
            _repository = repository;
        }

        public async Task UpdateAsync(TEntity entity, string userId = null)
        {
            var destination = await GetByIdAsync(entity.Id, userId);
            if (destination == null) return;
            destination = _modelMapper.Map(entity, destination);
            _repository.Update(destination);
        }

        public async Task<IEnumerable<TEntity>> GetByCreationTime(
            PaginationDomainModel pagination,
            string userId = null,
            bool orderByDescending = false)
        {
            var expression = _expressions.ByUserId(userId);
            var query = _repository.QueryByExpression(expression);
            query = _queryBuilder.BuildOrderByCreationTime(query, orderByDescending);
            query = query.Page(pagination);
            var list = await _repository.ToListAsync(query);
            return list;
        }

        public async Task<IEnumerable<TEntity>> GetByCreationTime(int count, bool orderByDescending = false)
        {
            var query = _repository.Query();
            query = _queryBuilder.BuildOrderByCreationTime(query, orderByDescending);
            query = query.Take(count);
            var list = await _repository.ToListAsync(query);
            return list;
        }

        public async Task<PaginationDomainModel> GetPaginationAsync(int pageNumber, int pageSize, string userId = null)
        {
            var expression = _expressions.ByUserId(userId);
            var count = await _repository.CountAsync(expression);

            var model = new PaginationDomainModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = count
            };

            model.Clamp();
            return model;
        }

        public async Task<TEntity> GetByIdAsync(long id, string userId = null)
        {
            var entity = await _repository.FirstOrDefaultAsync(_expressions.ById(id, userId));
            return entity;
        }

        public async Task<TEntity> GetByUserIdAsync(string userId)
        {
            var entity = await _repository.FirstOrDefaultAsync(_expressions.ByUserId(userId));
            return entity;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync()
        {
            var entity = await _repository.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<TEntity> RemoveAsync(long id, string userId = null)
        {
            var entity = await GetByIdAsync(id, userId);
            if (entity == null) return null;
            var removedEntity = _repository.Remove(entity);
            return removedEntity;
        }

        public TEntity Add(TEntity entity)
        {
            var addedEntity = _repository.Add(entity);
            return addedEntity;
        }
    }
}
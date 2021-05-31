using System.Linq;
using BlogApplication.Domain.Interfaces.Builders.Queries;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Base;

namespace BlogApplication.Domain.Builders.Queries
{
    public class EntityQueryBuilder<TEntity> : IEntityQueryBuilder<TEntity> where TEntity : EntityBase
    {
        private readonly IEntityExpressions<TEntity> _expressions;

        public EntityQueryBuilder(IEntityExpressions<TEntity> expressions)
        {
            _expressions = expressions;
        }

        public IQueryable<TEntity> BuildOrderByCreationTime(IQueryable<TEntity> query, bool orderByDescending)
        {
            if (orderByDescending)
                query = query
                    .OrderByDescending(_expressions.ByCreationTime())
                    .ThenByDescending(_expressions.ByModificationTime());
            else
                query = query
                    .OrderBy(_expressions.ByCreationTime())
                    .ThenBy(_expressions.ByModificationTime());

            return query;
        }
    }
}
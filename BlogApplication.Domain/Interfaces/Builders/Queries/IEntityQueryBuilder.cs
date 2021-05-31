using System.Linq;
using BlogApplication.Models.Entities.Base;

namespace BlogApplication.Domain.Interfaces.Builders.Queries
{
    public interface IEntityQueryBuilder<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> BuildOrderByCreationTime(IQueryable<TEntity> query, bool orderByDescending);
    }
}
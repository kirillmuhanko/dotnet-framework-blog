using System;
using System.Linq.Expressions;
using BlogApplication.Models.Entities.Base;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Interfaces.Expressions.Common
{
    public interface IEntityExpressions<TEntity> where TEntity : EntityBase
    {
        Expression<Func<TEntity, bool>> ById(long id, string userId = null);

        Expression<Func<TEntity, bool>> ByUserId(string userId);

        Expression<Func<TEntity, DateTime>> ByCreationTime();

        Expression<Func<TEntity, DateTime>> ByModificationTime();

        Expression<Func<TEntity, UserEntity>> ByUser();
    }
}
using System;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Base;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Expressions.Common
{
    public class EntityExpressions<TEntity> : IEntityExpressions<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, bool>> ById(long id, string userId = null)
        {
            if (userId != null)
                return t => t.Id.Equals(id) && t.AddedByUserId.Equals(userId);

            return t => t.Id.Equals(id);
        }

        public Expression<Func<TEntity, bool>> ByUserId(string userId)
        {
            if (userId != null)
                return t => t.AddedByUserId.Equals(userId);

            return t => !t.AddedByUserId.Equals(null);
        }

        public Expression<Func<TEntity, DateTime>> ByCreationTime()
        {
            return t => t.AddedDateTime;
        }

        public Expression<Func<TEntity, DateTime>> ByModificationTime()
        {
            return t => t.ModifiedDateTime;
        }

        public Expression<Func<TEntity, UserEntity>> ByUser()
        {
            return t => t.AddedByUser;
        }
    }
}
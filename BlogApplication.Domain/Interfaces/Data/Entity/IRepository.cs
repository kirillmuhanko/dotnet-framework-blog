using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogApplication.Models.Constants.StringSymbols;

namespace BlogApplication.Domain.Interfaces.Data.Entity
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = StringSymbols.Empty);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> QueryByExpression(Expression<Func<TEntity, bool>> expression);

        Task LoadCollectionAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty)
            where TProperty : class;

        Task LoadReferenceAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class;

        Task<IList<TEntity>> ToListAsync();

        Task<IList<TEntity>> ToListAsync(IQueryable<TEntity> query);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstOrDefaultAsync();

        TEntity Add(TEntity entity);

        TEntity Remove(TEntity entity);

        void AddOrUpdate(TEntity entity);

        void Load<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class;

        void Update(TEntity entity);
    }
}
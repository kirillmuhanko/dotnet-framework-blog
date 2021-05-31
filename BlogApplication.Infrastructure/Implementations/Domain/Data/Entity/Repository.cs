using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Infrastructure.Database;
using BlogApplication.Models.Constants.StringSymbols;

namespace BlogApplication.Infrastructure.Implementations.Domain.Data.Entity
{
    public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(BlogDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = StringSymbols.Empty)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) query = query.Where(filter);

            query = includeProperties
                .Split(new[] {CharSymbols.Comma}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            var list = orderBy != null ? orderBy(query).ToList() : query.ToList();
            return list;
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> QueryByExpression(Expression<Func<TEntity, bool>> expression)
        {
            var query = _dbSet.Where(expression);
            return query;
        }

        public async Task LoadCollectionAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty)
            where TProperty : class
        {
            await _context.Entry(entity).Collection(navigationProperty).LoadAsync();
        }

        public async Task LoadReferenceAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class
        {
            await _context.Entry(entity).Reference(navigationProperty).LoadAsync();
        }

        public async Task<IList<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            var list = await query.ToListAsync();
            return list;
        }

        public async Task<IList<TEntity>> ToListAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            var count = await _dbSet.CountAsync(expression);
            return count;
        }

        public async Task<int> CountAsync()
        {
            var count = await _dbSet.CountAsync();
            return count;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            var entity = await _dbSet.FirstOrDefaultAsync();
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            var addedEntity = _dbSet.Add(entity);
            _context.Entry(addedEntity).State = EntityState.Added;
            return addedEntity;
        }

        public TEntity Remove(TEntity entity)
        {
            var removedEntity = _dbSet.Remove(entity);
            return removedEntity;
        }

        public void AddOrUpdate(TEntity entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        public void Load<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class
        {
            _context.Entry(entity).Reference(navigationProperty).Load();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
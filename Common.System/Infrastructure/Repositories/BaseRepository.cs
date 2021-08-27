using Common.System.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.System.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext EntitiesContext { get; set; }

        public BaseRepository(DbContext entitiesContext)
        {
            EntitiesContext = entitiesContext;
        }

        public void Create(TEntity entity)
        {
            EntitiesContext.Set<TEntity>().Add(entity);
        }

        public bool Delete(TEntity entity)
        {
            EntitiesContext.Set<TEntity>().Remove(entity);
            return true;
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return EntitiesContext.Set<TEntity>().AsQueryable<TEntity>();
        }

        public async Task UpdateEntryAsync(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var entry = EntitiesContext.Entry(entity);

            EntitiesContext.Set<TEntity>().Attach(entity);

            foreach (var property in properties)
                entry.Property(property).IsModified = true;

            await EntitiesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await EntitiesContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await EntitiesContext.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task SaveAsync()
        {
            await EntitiesContext.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            EntitiesContext.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = null;

            if (expression != null)
                query = EntitiesContext.Set<TEntity>().Where(expression);
            else
                query = EntitiesContext.Set<TEntity>();

            if (includes != null)
                return await includes.Aggregate(query, (current, include) =>
                {
                    if (!string.IsNullOrEmpty("include"))
                        return current.Include(include);
                    else
                        return current;
                })
                    .AsNoTracking()
                    .ToListAsync();
            else
                return await query
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}

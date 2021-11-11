using API_DesignPatterns.Core.Interfaces.Repositories;
using API_DesignPatterns.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_DesignPatterns.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;

        public Repository(AppDbContext context)
        {
            Context = context;
        }

        public Task<T> AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            return Task.FromResult(entities);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> RemoveAsync(Guid id)
        {
            var entityToBeDeleted = await Context.Set<T>().FindAsync(id);
            if (entityToBeDeleted == null)
            {
                return entityToBeDeleted;
            }
            Context.Set<T>().Remove(entityToBeDeleted);

            return entityToBeDeleted;
        }

        public Task<T> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
            return Task.FromResult(entities);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            var entities = Context.Set<T>().Where(filter);

            return Task.FromResult(entities.Any());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Context.Set<T>().Find(id));
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            var entities = Context.Set<T>().AsNoTracking();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            return Task.FromResult(entities);
        }

        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return Task.FromResult(Context.Set<T>().FirstOrDefault(filter)); 
        }
    }
}

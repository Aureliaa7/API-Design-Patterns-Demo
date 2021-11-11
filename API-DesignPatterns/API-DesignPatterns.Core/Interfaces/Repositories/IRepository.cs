using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task<T> RemoveAsync(Guid id);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    }
}

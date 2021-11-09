﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> AddAsync(T entity);

        Task<T> RemoveAsync(Guid id);

        Task<T> UpdateAsync(T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    }
}
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hepsiburada.Infrastructure.Abstract
{
    public interface IEntityRepository<T, TType>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T> AddAsync(T entity);
        Task<int> AddRange(List<T> entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<IQueryable<T>> GetAllQueryablesAsync();
        Task<T> GetByIDAsync(TType id);
        Task<IList<T>> GetByIDListAsync(TType id);
        Task<T> SetStatusAsync(TType id, bool status);
        Task<int> SaveChangesAsync();
        void Update(T entity);
        IIncludableQueryable<T, TProperty> Include<TProperty>(
         Expression<Func<T, TProperty>> navigationPropertyPath);

    }
}

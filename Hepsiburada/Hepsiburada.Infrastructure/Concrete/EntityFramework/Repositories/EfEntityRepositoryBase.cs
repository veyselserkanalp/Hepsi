using Hepsiburada.Infrastructure.Abstract;
using Hepsiburada.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Hepsiburada.Infrastructure.Concrete.EntityFramework.Repositories
{
    public class EfEntityRepositoryBase<TEntity, TType> : IEntityRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>, new()
    {
        protected readonly DbContext _context = null;

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {

            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {

                Exception exception = new(ex.Message.ToString());
                throw exception;
            }

        }

        public async Task<int> AddRange(List<TEntity> entity)
        {
            await _context.Set<TEntity>().AddRangeAsync(entity);
            return _context.SaveChanges();

        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {

            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await (predicate == null ? _context.Set<TEntity>().CountAsync() : _context.Set<TEntity>().CountAsync(predicate));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();

        }

        public Task<IQueryable<TEntity>> GetAllQueryablesAsync()
        {
            var data = _context.Set<TEntity>().AsQueryable();
            return Task.FromResult(data);

        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

        public Task<TEntity> GetByIDAsync(TType id)
        {
            if (id is int)
            {
                var data = _context.Set<TEntity>().Where((y => Convert.ToInt32(y.Id) == Convert.ToInt32(id)));
                return Task.FromResult(data.FirstOrDefault());

            }
            else
            {
                var users = _context.Set<TEntity>().Where(y => y.Id.ToString() == id.ToString()).FirstOrDefault();
                return Task.FromResult(users);
            }

        }

        public Task<IList<TEntity>> GetByIDListAsync(TType id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            return entity;
        }

        public async Task<TEntity> SetStatusAsync(TType id, bool status)
        {
            var entity = GetByIDAsync(id).Result;
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            var resData = await _context.SaveChangesAsync();

            return resData;

        }

        public void Update(TEntity entity)
        {
            var updatedEtnitiy = _context.Entry(entity);
            updatedEtnitiy.State = EntityState.Detached;
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(entity, null) == null)
                {
                    updatedEtnitiy.Property(property.Name).IsModified = false;
                }
            }
        }

        private DbSet<TEntity> Entities => _context.Set<TEntity>();

        public IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            return Entities.Include(navigationPropertyPath);
        }


    }

}

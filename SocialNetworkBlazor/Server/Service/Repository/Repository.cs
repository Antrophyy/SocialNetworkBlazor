using Microsoft.EntityFrameworkCore;
using SocialNetworkBlazor.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Service.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context?.Set<TEntity>();
        }

        public async Task Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteById(object id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id).ConfigureAwait(false);
            await Delete(entityToDelete).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            if (orderBy == null)
                return await query.ToListAsync().ConfigureAwait(false);

            return await orderBy(query).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TType>> SelectData<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> whereExpression = null, string includeProperties = "") where TType : class
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (whereExpression == null)
                return await query.Select(select).ToListAsync().ConfigureAwait(false);

            return await query.Where(whereExpression).Select(select).ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetWithRawSql(string query, params object[] parameters)
        {
            return await _dbSet.FromSqlRaw(query, parameters).ToListAsync().ConfigureAwait(false);
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            _dbSet.UpdateRange(entitiesToUpdate);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> GetCount()
        {
            return await _dbSet.CountAsync().ConfigureAwait(false);
        }
    }
}

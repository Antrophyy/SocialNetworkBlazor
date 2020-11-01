using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Service.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Delete(TEntity entityToDelete);
        Task DeleteById(object id);
        Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IEnumerable<TType>> SelectData<TType>(Expression<Func<TEntity, TType>> selectExpression, Expression<Func<TEntity, bool>> where = null, string includeProperties = "") where TType : class;
        Task<TEntity> GetById(object id);
        Task<IEnumerable<TEntity>> GetWithRawSql(string query, params object[] parameters);
        Task Insert(TEntity entity);
        Task Update(TEntity entityToUpdate);
        Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
        Task<int> GetCount();
    }
}

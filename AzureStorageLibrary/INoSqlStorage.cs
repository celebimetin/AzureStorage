using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureStorageLibrary
{
    public interface INoSqlStorage<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(string rowKey, string partitionKey);
        Task<TEntity> Get(string rowKey, string partitionKey);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> exception);
    }
}
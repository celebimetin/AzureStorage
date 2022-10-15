using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace AzureStorageLibrary.Services
{
    public class TableStorage<TEntity> : INoSqlStorage<TEntity> where TEntity : TableEntity, new()
    {
        private readonly CloudTableClient _client;
        private readonly CloudTable _table;

        public TableStorage()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConnectionStrings.AzureStorageConnectionString);

            _client = cloudStorageAccount.CreateCloudTableClient();
            _table = _client.GetTableReference(typeof(TEntity).Name);
            _table.CreateIfNotExists();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var operation = TableOperation.InsertOrMerge(entity);
            var execute = await _table.ExecuteAsync(operation);
            return execute.Result as TEntity;
        }

        public async Task Delete(string rowKey, string partitionKey)
        {
            var entity = await Get(rowKey, partitionKey);
            var operation = TableOperation.Delete(entity);
            await _table.ExecuteAsync(operation);
        }

        public async Task<TEntity> Get(string rowKey, string partitionKey)
        {
            var operation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var execute = await _table.ExecuteAsync(operation);
            return execute.Result as TEntity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _table.CreateQuery<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> exception)
        {
            return _table.CreateQuery<TEntity>().Where(exception);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);
            var execute = await _table.ExecuteAsync(operation);
            return execute.Result as TEntity;
        }
    }
}
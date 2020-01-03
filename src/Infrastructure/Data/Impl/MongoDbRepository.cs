using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Impl
{
    public class MongoDbRepository<T> : IReadRepository<T> where T : IReadEntity
    {
        private IMongoCollection<T> _collection = null;

        public MongoDbRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _collection.FindAsync(predicate);
            return result.ToList();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _collection.Find(x => x.Id == id).SingleAsync();
        }
    }
}

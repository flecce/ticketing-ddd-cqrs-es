using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IReadRepository<T> where T : IReadEntity
    {
        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Find entity by clause.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get aggregate by id.
        /// </summary>
        /// <param name="id">Aggregate id.</param>
        /// <returns>Aggregate.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);
    }
}

using Infrastructure.Domain;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IRepository<T> where T : IAggregate
    {
        /// <summary>
        /// Get aggregate by id.
        /// </summary>
        /// <param name="id">Aggregate id.</param>
        /// <returns>Aggregate.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Save aggregate.
        /// </summary>
        /// <param name="aggregate">Aggregate to save.</param>
        /// <returns>If true aggregate was saved.</returns>
        Task<bool> SaveAsync(T aggregate);
    }
}

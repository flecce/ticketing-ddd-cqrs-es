using Infrastructure.Domain;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IRepository<T> where T : IAggregate
    {
        Task<T> GetByIdAsync(Guid id);
        Task SaveAsync(T aggregate);
    }
}

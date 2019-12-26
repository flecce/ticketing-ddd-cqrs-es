using System;

namespace Infrastructure.Domain
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}

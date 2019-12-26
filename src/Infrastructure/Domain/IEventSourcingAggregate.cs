using System.Collections.Generic;

namespace Infrastructure.Domain
{
    internal interface IEventSourcingAggregate
    {
        int Version { get; }
        void ApplyEvent(IDomainEvent @event, long version);
        IEnumerable<IDomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}

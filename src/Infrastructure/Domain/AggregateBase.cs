using System;
using System.Collections.Generic;

namespace Infrastructure.Domain
{
    public abstract class AggregateBase : IAggregate, IEventSourcingAggregate
    {
        public Guid Id { get; protected set; }
        public int Version { get; private set; }
        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        public void ApplyEvent(IDomainEvent @event)
        {
            if (@event == null)
                return;

            ((dynamic)this).Apply((dynamic)@event);
            Version++;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            _uncommittedEvents.Add(@event);
            Version++;
        }
    }
}

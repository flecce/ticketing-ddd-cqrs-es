using System;
using System.Collections.Generic;

namespace Infrastructure.Domain
{
    public abstract class AggregateBase : IAggregate, IEventSourcingAggregate
    {
        public const long NewAggregateVersion = -1;
        private long _version = NewAggregateVersion;
        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        public void ApplyEvent(IDomainEvent @event, long version)
        {
            //if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            //{
            ((dynamic)this).Apply((dynamic)@event);
            _version = version;
            //}
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
            _version++;
            _uncommittedEvents.Add(@event);
        }
    }
}

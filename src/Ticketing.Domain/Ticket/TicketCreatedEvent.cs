using Infrastructure.Domain;
using System;

namespace Ticketing.Domain.Ticket
{
    public class TicketCreatedEvent : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Title { get; private set; }

        public TicketCreatedEvent(Guid aggregateId, Guid customerId, string title)
        {
            AggregateId = aggregateId;
            CustomerId = customerId;
            Title = title;
        }
    }
}

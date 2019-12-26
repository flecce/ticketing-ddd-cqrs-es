using Infrastructure.Domain;
using System;

namespace Domain.Ticket
{
    public class ActivityAddedEvent : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Effort { get; private set; }

        public ActivityAddedEvent(Guid aggregateId, string title, string description, int effort)
        {
            AggregateId = aggregateId;
            Title = title;
            Description = description;
            Effort = effort;
        }
    }
}

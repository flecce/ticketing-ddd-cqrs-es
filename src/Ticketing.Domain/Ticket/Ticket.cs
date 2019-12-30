using Infrastructure.Domain;
using System;
using System.Collections.Generic;

namespace Ticketing.Domain.Ticket
{
    public class Ticket : AggregateBase
    {
        public Guid CustomerId { get; private set; }
        public string Title { get; private set; }
        public List<Activity> Activities { get; private set; }

        public Ticket()
        {
            Activities = new List<Activity>();
        }

        public Ticket(Guid customerId, string title) : this()
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Title = title;

            RaiseEvent(new TicketCreatedEvent(Id, customerId, title));
        }

        public void Apply(TicketCreatedEvent @event)
        {
            Id = @event.AggregateId;
            CustomerId = @event.CustomerId;
            Title = @event.Title;
        }

        public void Apply(ActivityAddedEvent @event)
        {
            Activities.Add(new Activity(@event.Title, @event.Description, @event.Effort));
        }

        public void AddActivity(string title, string description, int effort = 0)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Activities.Add(new Activity(title, description, effort));
            RaiseEvent(new ActivityAddedEvent(Id, title, description, effort));
        }
    }
}

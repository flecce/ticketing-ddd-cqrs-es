using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Api.Application.ReadModel.Models;
using Ticketing.Domain.Ticket;

namespace Ticketing.Api.Application.DomainEventHandlers
{
    public class ActivityAddedDomainEventHandler : INotificationHandler<ActivityAddedEvent>
    {
        private readonly IReadRepository<TicketReadModel> _ticketReadRepository;

        public ActivityAddedDomainEventHandler(IReadRepository<TicketReadModel> ticketReadRepository)
        {
            _ticketReadRepository = ticketReadRepository ?? throw new ArgumentException(nameof(ticketReadRepository));
        }

        public async Task Handle(ActivityAddedEvent notification, CancellationToken cancellationToken)
        {
            var ticket = await _ticketReadRepository.GetByIdAsync(notification.AggregateId);
            if (ticket == null)
                return;

            if (ticket.Activities == null)
                ticket.Activities = new List<ActivityReadModel>();

            ticket.Activities.Add(new ActivityReadModel
            {
                Title = notification.Title,
                Description = notification.Description,
                Effort = notification.Effort
            });

            await _ticketReadRepository.UpdateAsync(ticket);
        }
    }
}

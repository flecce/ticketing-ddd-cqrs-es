using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Api.Application.ReadModel.Models;
using Ticketing.Domain.Ticket;

namespace Ticketing.Api.Application.DomainEventHandlers
{
    public class TicketCreatedDomainEventHandler : INotificationHandler<TicketCreatedEvent>
    {
        private readonly IReadRepository<TicketReadModel> _ticketReadRepository;

        public TicketCreatedDomainEventHandler(IReadRepository<TicketReadModel> ticketReadRepository)
        {
            _ticketReadRepository = ticketReadRepository ?? throw new ArgumentException(nameof(ticketReadRepository));
        }

        public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _ticketReadRepository.AddAsync(new TicketReadModel
            {
                Id = notification.AggregateId,
                Title = notification.Title,
                Description = string.Empty
            });
        }
    }
}

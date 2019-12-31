using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Domain.Ticket;

namespace Ticketing.Api.Application.DomainEventHandlers
{
    public class TicketCreatedDomainEventHandler : INotificationHandler<TicketCreatedEvent>
    {
        public Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
        {
            // TODO: update read model
            return null;
        }
    }
}

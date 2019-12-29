using Domain.Ticket;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ticketing.Api.Application.Commands
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, bool>
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public CreateTicketCommandHandler(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<bool> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket newTicket = new Ticket(Guid.NewGuid(), request.Title);
            return await _ticketRepository.SaveAsync(newTicket);
        }
    }
}

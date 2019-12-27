using Domain.Ticket;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ticketing.Api.Application.Commands
{
    public class AddActivityCommandHandler : IRequestHandler<AddActivitytCommand, bool>
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public AddActivityCommandHandler(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<bool> Handle(AddActivitytCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null)
                return false;

            ticket.AddActivity(request.Title, request.Description, request.Effort);
            return await _ticketRepository.SaveAsync(ticket);
        }
    }
}

using Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Api.Application.Queries.Interfaces;
using Ticketing.Api.Application.ReadModel.Models;

namespace Ticketing.Api.Application.ReadModel.Impl
{
    public class TicketReader : ITicketReader
    {
        private readonly IReadRepository<TicketReadModel> _ticketReadRepository;

        public TicketReader(IReadRepository<TicketReadModel> ticketReadRepository)
        {
            _ticketReadRepository = ticketReadRepository ?? throw new ArgumentException(nameof(ticketReadRepository));
        }

        public async Task<IEnumerable<TicketReadModel>> GetTickets()
        {
            return await _ticketReadRepository.FindAsync(_ => true);
        }
    }
}

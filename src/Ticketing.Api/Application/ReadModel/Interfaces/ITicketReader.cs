using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Api.Application.ReadModel.Models;

namespace Ticketing.Api.Application.Queries.Interfaces
{
    public interface ITicketReader
    {
        Task<IEnumerable<TicketReadModel>> GetTickets();
    }
}

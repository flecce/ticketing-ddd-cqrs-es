using System.Threading.Tasks;
using Ticketing.Api.Application.Queries.Interfaces;
using Ticketing.Api.Application.Queries.ViewModels;

namespace Ticketing.Api.Application.Queries.Impl
{
    public class TicketQuery : ITicketQuery
    {
        public Task<TicketViewModel> GetTickets()
        {
            throw new System.NotImplementedException();
        }
    }
}

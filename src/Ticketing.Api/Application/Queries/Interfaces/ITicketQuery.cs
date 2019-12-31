using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing.Api.Application.Queries.ViewModels;

namespace Ticketing.Api.Application.Queries.Interfaces
{
    public interface ITicketQuery
    {
        Task<TicketViewModel> GetTickets();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Domain.Ticket;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ticketing.Api.Controllers
{
    [DataContract]
    public class CreateTicketCommand
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketController(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
        {
            Ticket newTicket = new Ticket(Guid.NewGuid(), command.Title);
            newTicket.AddActivity("Init", "First step", 0);

            await _ticketRepository.SaveAsync(newTicket);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return Ok(ticket);
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Ticketing.Api.Application.Commands;

namespace Ticketing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IMediator _mediator;

        public TicketController(
            ILogger<TicketController> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(AddActivitytCommand),
                command);

            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("addactivity")]
        [HttpPost]
        public async Task<IActionResult> AddActivity(AddActivitytCommand command)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(AddActivitytCommand),
                command);

            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetTicketById(Guid id)
        //{
        //    return Ok();
        //}
    }
}
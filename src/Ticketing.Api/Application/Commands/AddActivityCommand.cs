using MediatR;
using System;
using System.Runtime.Serialization;

namespace Ticketing.Api.Application.Commands
{
    [DataContract]
    public class AddActivitytCommand : IRequest<bool>
    {
        [DataMember(Name = "ticketId")]
        public Guid TicketId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "effort")]
        public int Effort { get; set; }
    }
}

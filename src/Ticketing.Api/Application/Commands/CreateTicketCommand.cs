using MediatR;
using System;
using System.Runtime.Serialization;

namespace Ticketing.Api.Application.Commands
{
    [DataContract]
    public class CreateTicketCommand : IRequest<bool>
    {
        [DataMember(Name = "customerId")]
        public Guid CustomerId { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}

using MediatR;
using System.Runtime.Serialization;

namespace Ticketing.Api.Application.Commands
{
    [DataContract]
    public class CreateTicketCommand : IRequest<bool>
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}

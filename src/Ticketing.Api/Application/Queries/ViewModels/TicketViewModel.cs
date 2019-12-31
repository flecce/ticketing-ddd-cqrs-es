using System;
using System.Runtime.Serialization;

namespace Ticketing.Api.Application.Queries.ViewModels
{
    [DataContract]
    public class TicketViewModel
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}

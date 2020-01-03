using Infrastructure.Data.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Ticketing.Api.Application.ReadModel.Models
{
    [DataContract]
    public class TicketReadModel : IReadEntity
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}

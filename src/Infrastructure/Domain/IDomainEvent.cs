using MediatR;
using System;

namespace Infrastructure.Domain
{
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// The identifier of the aggregate which has generated the event.
        /// </summary>
        Guid AggregateId { get; }
    }
}

using Infrastructure.Domain;
using System;

namespace Ticketing.Domain.Customer
{
    public class Customer : AggregateBase
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
    }
}

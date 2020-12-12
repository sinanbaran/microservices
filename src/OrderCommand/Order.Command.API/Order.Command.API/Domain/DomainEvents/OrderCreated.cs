using System;
using Order.Command.API.Core;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.DomainEvents
{
    public class OrderCreated : DomainEvent
    {
        public Guid AggregateId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Owner { get; set; }
        public string State { get; set; }
    }
}

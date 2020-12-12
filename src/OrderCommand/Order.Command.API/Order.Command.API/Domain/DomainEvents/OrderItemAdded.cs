using System;
using Order.Command.API.Core;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.DomainEvents
{
    public class OrderItemAdded : DomainEvent
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Guid AggregateId { get; set; }
    }
}

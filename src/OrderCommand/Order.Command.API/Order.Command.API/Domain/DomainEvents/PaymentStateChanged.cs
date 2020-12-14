using System;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.DomainEvents
{

    public class PaymentStateChanged : DomainEvent
    {
        public Guid AggregateId { get; set; }
        public string State { get; set; }
    }
}

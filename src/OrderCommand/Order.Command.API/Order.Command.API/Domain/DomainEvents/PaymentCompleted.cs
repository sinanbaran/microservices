using System;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.DomainEvents
{

    public class PaymentCompleted : DomainEvent
    {
        public Guid AggregateId { get; set; }
    }
}

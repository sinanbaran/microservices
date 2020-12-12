using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Command.API.Core;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.DomainEvents
{

    public class PaymentCompleted : DomainEvent
    {
        public Guid AggregateId { get; set; }
    }
}

using System;
using Order.Command.API.Core.Events;

namespace Order.Command.API.Core.Domain
{
    public class DomainEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
    }
}

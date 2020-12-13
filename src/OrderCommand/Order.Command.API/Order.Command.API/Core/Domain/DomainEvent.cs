using System;

namespace Order.Command.API.Core.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
    }
}

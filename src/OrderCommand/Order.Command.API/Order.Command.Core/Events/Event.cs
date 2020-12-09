using System;

namespace Order.Command.Core.Events
{
    public class Event : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
    }
}

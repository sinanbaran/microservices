using System;

namespace Order.Command.API.Core.Events
{
    public class Event : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
    }
}

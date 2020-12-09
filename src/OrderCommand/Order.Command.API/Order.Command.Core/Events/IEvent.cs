using System;

namespace Order.Command.Core.Events
{
    public interface IEvent
    {
        Guid EventId { get; set; }
    }
}
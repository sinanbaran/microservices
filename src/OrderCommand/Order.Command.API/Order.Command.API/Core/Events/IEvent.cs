using System;

namespace Order.Command.API.Core.Events
{
    public interface IEvent
    {
        Guid EventId { get; set; }
    }
}
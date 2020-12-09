using System.Collections.Generic;
using Order.Command.Core.Events;

namespace Order.Command.Core.Commands
{
    public class CommandResponse
    {
        public IList<IEvent> Events { get; set; } = new List<IEvent>();
        public object Result { get; set; }
        public static CommandResponse Success { get; set; } = new CommandResponse(string.Empty);

        public CommandResponse AddEvent(IEvent @event)
        {
            Events.Add(@event);
            return this;
        }
        public CommandResponse(object result)
        {
            Result = result;
        }
        public CommandResponse(IEvent @event, object result = null)
        {
            Events = new List<IEvent>
            {
                @event
            };

            Result = result;
        }
    }
}

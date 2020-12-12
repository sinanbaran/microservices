using System.Collections.Generic;
using System.Linq;
using Order.Command.API.Core.Events;

namespace Order.Command.API.Core.Commands
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
        public CommandResponse()
        {}
    }

}

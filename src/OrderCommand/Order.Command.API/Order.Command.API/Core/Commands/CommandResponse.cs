using System.Collections.Generic;
using Order.Command.API.Core.Events;

namespace Order.Command.API.Core.Commands
{
    public class CommandResponse
    {
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public object Result { get; set; }
        public static CommandResponse Success { get; set; } = new CommandResponse(string.Empty);

        public CommandResponse(object result)
        {
            Result = result;
        }
        public CommandResponse()
        { }
    }

}

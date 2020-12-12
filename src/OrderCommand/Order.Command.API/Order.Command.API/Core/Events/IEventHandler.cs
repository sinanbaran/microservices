using System.Threading.Tasks;

namespace Order.Command.API.Core.Events
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
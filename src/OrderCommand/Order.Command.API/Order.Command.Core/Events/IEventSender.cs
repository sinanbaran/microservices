using System.Threading.Tasks;

namespace Order.Command.Core.Events
{
    public interface IEventSender
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}